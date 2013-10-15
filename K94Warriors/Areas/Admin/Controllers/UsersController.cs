using K94Warriors.Controllers;
using K94Warriors.Data.Contracts;
using K94Warriors.Enums;
using K94Warriors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<UserType> _userTypeRepo;

        public UsersController(IRepository<User> userRepo,
            IRepository<UserType> userTypeRepo)
        {
            _userRepo = userRepo;
            _userTypeRepo = userTypeRepo;
        }
        
        public ActionResult Index()
        {
            var model = _userRepo.GetAll();
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.UserTypesSelectList = new SelectList(_userTypeRepo.GetAll(), "ID", "Name", (int)UserTypeEnum.Volunteer);

            return View();
        }


    }
}
