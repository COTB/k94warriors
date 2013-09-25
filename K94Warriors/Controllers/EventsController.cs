using System;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class EventsController : Controller
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogEvent> _dogEventsRepo;

        public EventsController(IRepository<DogProfile> dogProfileRepo,
                                IRepository<DogEvent> dogEventsRepo)
        {
            if (dogProfileRepo == null)
                throw new ArgumentNullException("dogProfileRepo");
            _dogProfileRepo = dogProfileRepo;

            if (dogEventsRepo == null)
                throw new ArgumentNullException("dogEventsRepo");
            _dogEventsRepo = dogEventsRepo;
        }

        //
        // GET: /Events/

        public ActionResult Index(int dogProfileId)
        {
            return View();
        }

        public ActionResult Create(int dogProfileId)
        {
            // verify dog exists
            var dog = _dogProfileRepo.GetById(dogProfileId);
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            return View(new DogEvent { DogProfileID = dogProfileId });
        }

        [HttpPost]
        public ActionResult Create(DogEvent model)
        {
            return View();
        }

        public ActionResult Edit(int dogEventId)
        {
            var model = _dogEventsRepo.GetById(dogEventId);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DogEvent model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int dogEventId, int? dogProfileId)
        {
            _dogEventsRepo.Delete(dogEventId);

            return dogProfileId.HasValue
                       ? RedirectToAction("Index", new { dogProfileId = dogProfileId.Value })
                       : RedirectToAction("Index", "Dog");
        }
    }
}
