using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public partial class SkillsController : Controller
    {
        //
        // GET: /Skills/

        public virtual ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrUpdateSkill(DogSkill dogSkill)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetSkill(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }
    }
}
