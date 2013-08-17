using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
