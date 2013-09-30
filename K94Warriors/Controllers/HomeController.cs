using System;
using System.Data.Objects;
using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
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
    }
}