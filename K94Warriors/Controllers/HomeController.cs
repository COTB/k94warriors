using System;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class HomeController : BaseController
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
            var events = _dogEventRepo
                .Where(f => f.IsComplete == false)
                .Include(i => i.DogProfile)
                .ToList();

            return View(events);
        }
    }
}