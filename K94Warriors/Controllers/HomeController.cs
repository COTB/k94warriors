using System;
using System.Data.Objects;
using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<DogEvent> _dogEventRepo;

        public HomeController(IRepository<DogEvent> dogEventRepo)
        {
            if (dogEventRepo == null)
                throw new ArgumentNullException("dogEventRepo");
            _dogEventRepo = dogEventRepo;
        }

        public virtual ActionResult Index()
        {
            IQueryable<DogEvent> events =
                _dogEventRepo.Where(
                    f => f.EventDate > DateTime.UtcNow && f.EventDate < EntityFunctions.AddDays(DateTime.Now, 7));

            return View(events);
        }

        [OutputCache(Duration = 240)]
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