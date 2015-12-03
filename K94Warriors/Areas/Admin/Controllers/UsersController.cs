using K94Warriors.Controllers;
using K94Warriors.Data.Contracts;
using K94Warriors.Enums;
using K94Warriors.Filters;
using K94Warriors.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace K94Warriors.Areas.Admin.Controllers
{
    [InitializeSimpleMembership]
    [K9Authorize(Roles = "Admin")]
    public partial class UsersController : BaseController
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<UserType> _userTypeRepo;

        public UsersController(IRepository<User> userRepo,
            IRepository<UserType> userTypeRepo)
        {
            _userRepo = userRepo;
            _userTypeRepo = userTypeRepo;
        }

        public virtual ActionResult Index()
        {
            var model = _userRepo.GetAll();

            ViewBag.CurrentUserId = CurrentUserId;

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            ViewBag.UserTypesSelectList = new SelectList(_userTypeRepo.GetAll(), "ID", "Name", (int)UserTypeEnum.Volunteer);

            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(User model, string password)
        {
            if (string.IsNullOrEmpty(password))
                ModelState.AddModelError("Password", "You must provide a password for this user.");

            if (!ModelState.IsValid)
            {
                ViewBag.UserTypesSelectList = new SelectList(_userTypeRepo.GetAll(), "ID", "Name", (int)UserTypeEnum.Volunteer);

                return View(model);
            }

            WebSecurity.CreateUserAndAccount(model.Email, password,
            new
            {
                UserTypeId = model.UserTypeID,
                CreatedTimeUTC = DateTime.UtcNow,
                model.DisplayName,
                model.Phone
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var model = _userRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            ViewBag.UserTypesSelectList = new SelectList(_userTypeRepo.GetAll(), "ID", "Name", (int)UserTypeEnum.Volunteer);
            ViewBag.CurrentUserId = CurrentUserId;

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(User model, string password, int existingUserTypeID)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserTypesSelectList = new SelectList(_userTypeRepo.GetAll(), "ID", "Name", (int)UserTypeEnum.Volunteer);

                return View(model);
            }

            if (!string.IsNullOrEmpty(password))
            {
                string token = WebSecurity.GeneratePasswordResetToken(model.Email);
                WebSecurity.ResetPassword(token, password);
            }

            // in case of editing same user, the UserTypeID is not sent since select is disabled
            if (model.UserTypeID == 0)
                model.UserTypeID = existingUserTypeID;

            _userRepo.Update(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            if (id == CurrentUserId)
                throw new InvalidOperationException("Can't delete current user!");

            var user = _userRepo.GetById(id);

            if (user == null)
                return HttpNotFound();

            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.Email); // deletes record from webpages_Membership table
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(user.Email, true); // deletes record from Users table

            return RedirectToAction("Index");
        }
    }
}
