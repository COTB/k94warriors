using K94Warriors.Enums;
using K94Warriors.Filters;
using K94Warriors.Models.Accounts;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K94Warriors.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public partial class AccountController : Controller
    {
        private const string DefaultUserEmail = "admin@k9sforwarriors.org";

        // 
        // GET: /Account/CreateInitialAccount
        [AllowAnonymous]
        public virtual ActionResult CreateInitialAccount()
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(DefaultUserEmail));

            if (hasLocalAccount)
                throw new InvalidOperationException("Default initial account already exists!");

            WebSecurity.CreateUserAndAccount(DefaultUserEmail, "admin", new
            {
                UserTypeId = UserTypeEnum.Administrator,
                DisplayName = DefaultUserEmail,
                Email = DefaultUserEmail,
                CreatedTimeUTC = DateTime.UtcNow
            });

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                WebSecurity.Logout();
                Session.Abandon();
                HttpContext.User = null;
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff


        public virtual ActionResult LogOff()
        {
            WebSecurity.Logout();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Manage

        public virtual ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                          ? "Your password has been set."
                          : message == ManageMessageId.RemoveLoginSuccess
                                ? "The external login was removed."
                                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword,
                                                                             model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                var state = ModelState["OldPassword"];
                state?.Errors.Clear();

                if (!ModelState.IsValid)
                    return View(model);

                try
                {
                    WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                    return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", $"Unable to create local account. An account with the name \"{User.Identity.Name}\" may already exist.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; }
            public string ReturnUrl { get; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }
    }
}