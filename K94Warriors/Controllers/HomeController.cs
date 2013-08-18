using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public partial class HomeController : Controller
    {
        private IRepository<DogEvent> _dogEventRepo;

        public HomeController()
        {
            _dogEventRepo = RepoResolver.GetRepository<DogEvent>();
        }

        public virtual ActionResult Index()
        {
            var events =
                _dogEventRepo.Where(f => f.EventDate > DateTime.UtcNow && f.EventDate < EntityFunctions.AddDays(DateTime.Now, 7));

            return View(events);
        }

        [OutputCache(Duration=240)]
        public virtual ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        [OutputCache(Duration = 240)]
        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
