// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace K94Warriors.Controllers
{
    public partial class AccountController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AccountController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Login()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Login);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Disassociate()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Disassociate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Manage()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Manage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExternalLogin()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLogin);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExternalLoginCallback()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginCallback);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExternalLoginConfirmation()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginConfirmation);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExternalLoginsList()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginsList);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountController Actions { get { return MVC.Account; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Account";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Account";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Login = "Login";
            public readonly string LogOff = "LogOff";
            public readonly string Register = "Register";
            public readonly string Disassociate = "Disassociate";
            public readonly string Manage = "Manage";
            public readonly string ExternalLogin = "ExternalLogin";
            public readonly string ExternalLoginCallback = "ExternalLoginCallback";
            public readonly string ExternalLoginConfirmation = "ExternalLoginConfirmation";
            public readonly string ExternalLoginFailure = "ExternalLoginFailure";
            public readonly string ExternalLoginsList = "ExternalLoginsList";
            public readonly string RemoveExternalLogins = "RemoveExternalLogins";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Login = "Login";
            public const string LogOff = "LogOff";
            public const string Register = "Register";
            public const string Disassociate = "Disassociate";
            public const string Manage = "Manage";
            public const string ExternalLogin = "ExternalLogin";
            public const string ExternalLoginCallback = "ExternalLoginCallback";
            public const string ExternalLoginConfirmation = "ExternalLoginConfirmation";
            public const string ExternalLoginFailure = "ExternalLoginFailure";
            public const string ExternalLoginsList = "ExternalLoginsList";
            public const string RemoveExternalLogins = "RemoveExternalLogins";
        }


        static readonly ActionParamsClass_Login s_params_Login = new ActionParamsClass_Login();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Login LoginParams { get { return s_params_Login; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Login
        {
            public readonly string returnUrl = "returnUrl";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Register s_params_Register = new ActionParamsClass_Register();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Register RegisterParams { get { return s_params_Register; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Register
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Disassociate s_params_Disassociate = new ActionParamsClass_Disassociate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Disassociate DisassociateParams { get { return s_params_Disassociate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Disassociate
        {
            public readonly string provider = "provider";
            public readonly string providerUserId = "providerUserId";
        }
        static readonly ActionParamsClass_Manage s_params_Manage = new ActionParamsClass_Manage();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Manage ManageParams { get { return s_params_Manage; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Manage
        {
            public readonly string message = "message";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ExternalLogin s_params_ExternalLogin = new ActionParamsClass_ExternalLogin();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExternalLogin ExternalLoginParams { get { return s_params_ExternalLogin; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExternalLogin
        {
            public readonly string provider = "provider";
            public readonly string returnUrl = "returnUrl";
        }
        static readonly ActionParamsClass_ExternalLoginCallback s_params_ExternalLoginCallback = new ActionParamsClass_ExternalLoginCallback();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExternalLoginCallback ExternalLoginCallbackParams { get { return s_params_ExternalLoginCallback; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExternalLoginCallback
        {
            public readonly string returnUrl = "returnUrl";
        }
        static readonly ActionParamsClass_ExternalLoginConfirmation s_params_ExternalLoginConfirmation = new ActionParamsClass_ExternalLoginConfirmation();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExternalLoginConfirmation ExternalLoginConfirmationParams { get { return s_params_ExternalLoginConfirmation; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExternalLoginConfirmation
        {
            public readonly string model = "model";
            public readonly string returnUrl = "returnUrl";
        }
        static readonly ActionParamsClass_ExternalLoginsList s_params_ExternalLoginsList = new ActionParamsClass_ExternalLoginsList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExternalLoginsList ExternalLoginsListParams { get { return s_params_ExternalLoginsList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExternalLoginsList
        {
            public readonly string returnUrl = "returnUrl";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _ChangePasswordPartial = "_ChangePasswordPartial";
                public readonly string _ExternalLoginsListPartial = "_ExternalLoginsListPartial";
                public readonly string _RemoveExternalLoginsPartial = "_RemoveExternalLoginsPartial";
                public readonly string _SetPasswordPartial = "_SetPasswordPartial";
                public readonly string ExternalLoginConfirmation = "ExternalLoginConfirmation";
                public readonly string ExternalLoginFailure = "ExternalLoginFailure";
                public readonly string Login = "Login";
                public readonly string Manage = "Manage";
                public readonly string Register = "Register";
            }
            public readonly string _ChangePasswordPartial = "~/Views/Account/_ChangePasswordPartial.cshtml";
            public readonly string _ExternalLoginsListPartial = "~/Views/Account/_ExternalLoginsListPartial.cshtml";
            public readonly string _RemoveExternalLoginsPartial = "~/Views/Account/_RemoveExternalLoginsPartial.cshtml";
            public readonly string _SetPasswordPartial = "~/Views/Account/_SetPasswordPartial.cshtml";
            public readonly string ExternalLoginConfirmation = "~/Views/Account/ExternalLoginConfirmation.cshtml";
            public readonly string ExternalLoginFailure = "~/Views/Account/ExternalLoginFailure.cshtml";
            public readonly string Login = "~/Views/Account/Login.cshtml";
            public readonly string Manage = "~/Views/Account/Manage.cshtml";
            public readonly string Register = "~/Views/Account/Register.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_AccountController : K94Warriors.Controllers.AccountController
    {
        public T4MVC_AccountController() : base(Dummy.Instance) { }

        partial void LoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string returnUrl);

        public override System.Web.Mvc.ActionResult Login(string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Login);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            LoginOverride(callInfo, returnUrl);
            return callInfo;
        }

        partial void LoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Models.Accounts.LoginModel model, string returnUrl);

        public override System.Web.Mvc.ActionResult Login(K94Warriors.Models.Accounts.LoginModel model, string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Login);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            LoginOverride(callInfo, model, returnUrl);
            return callInfo;
        }

        partial void LogOffOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult LogOff()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LogOff);
            LogOffOverride(callInfo);
            return callInfo;
        }

        partial void RegisterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Register()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Register);
            RegisterOverride(callInfo);
            return callInfo;
        }

        partial void RegisterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Models.Accounts.RegisterModel model);

        public override System.Web.Mvc.ActionResult Register(K94Warriors.Models.Accounts.RegisterModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Register);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            RegisterOverride(callInfo, model);
            return callInfo;
        }

        partial void DisassociateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string provider, string providerUserId);

        public override System.Web.Mvc.ActionResult Disassociate(string provider, string providerUserId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Disassociate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "provider", provider);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "providerUserId", providerUserId);
            DisassociateOverride(callInfo, provider, providerUserId);
            return callInfo;
        }

        partial void ManageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Controllers.AccountController.ManageMessageId? message);

        public override System.Web.Mvc.ActionResult Manage(K94Warriors.Controllers.AccountController.ManageMessageId? message)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Manage);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "message", message);
            ManageOverride(callInfo, message);
            return callInfo;
        }

        partial void ManageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Models.Accounts.LocalPasswordModel model);

        public override System.Web.Mvc.ActionResult Manage(K94Warriors.Models.Accounts.LocalPasswordModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Manage);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ManageOverride(callInfo, model);
            return callInfo;
        }

        partial void ExternalLoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string provider, string returnUrl);

        public override System.Web.Mvc.ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLogin);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "provider", provider);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            ExternalLoginOverride(callInfo, provider, returnUrl);
            return callInfo;
        }

        partial void ExternalLoginCallbackOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string returnUrl);

        public override System.Web.Mvc.ActionResult ExternalLoginCallback(string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginCallback);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            ExternalLoginCallbackOverride(callInfo, returnUrl);
            return callInfo;
        }

        partial void ExternalLoginConfirmationOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, K94Warriors.Models.Accounts.RegisterExternalLoginModel model, string returnUrl);

        public override System.Web.Mvc.ActionResult ExternalLoginConfirmation(K94Warriors.Models.Accounts.RegisterExternalLoginModel model, string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginConfirmation);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            ExternalLoginConfirmationOverride(callInfo, model, returnUrl);
            return callInfo;
        }

        partial void ExternalLoginFailureOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult ExternalLoginFailure()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginFailure);
            ExternalLoginFailureOverride(callInfo);
            return callInfo;
        }

        partial void ExternalLoginsListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string returnUrl);

        public override System.Web.Mvc.ActionResult ExternalLoginsList(string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExternalLoginsList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            ExternalLoginsListOverride(callInfo, returnUrl);
            return callInfo;
        }

        partial void RemoveExternalLoginsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult RemoveExternalLogins()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveExternalLogins);
            RemoveExternalLoginsOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
