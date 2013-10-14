using System;
using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogEvent> _dogEventsRepo;
        private readonly IRepository<EventType> _eventTypesRepo;

        public EventsController(IRepository<DogProfile> dogProfileRepo,
                                IRepository<DogEvent> dogEventsRepo,
                                IRepository<EventType> eventTypesRepo)
        {
            _dogProfileRepo = dogProfileRepo;
            _dogEventsRepo = dogEventsRepo;
            _eventTypesRepo = eventTypesRepo;
        }

        //
        // GET: /Events?dog={dogProfileId}

        public ActionResult Index(DogProfile dog)
        {
            // verify dog exists
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            var model = _dogEventsRepo.Where(d => d.DogProfileID == dog.ProfileID).OrderByDescending(i => i.EventDate).ToList();

            SetDogViewBag(dog);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(DogProfile dog)
        {
            // verify dog exists
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            SetDogViewBag(dog);

            ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", null);

            var model = new DogEvent
            {
                DogProfileID = dog.ProfileID,
                EventDate = DateTime.Now.Date
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DogEvent model)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", null);

                return View(model);
            }

            _dogEventsRepo.Insert(model);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }

        public ActionResult Edit(int dogEventId)
        {
            var model = _dogEventsRepo.GetById(dogEventId);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            SetDogViewBag(dog);

            ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", model.EventTypeId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DogEvent model)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", null);

                return View(model);
            }

            _dogEventsRepo.Update(model);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }

        [HttpPost]
        public ActionResult Delete(int dogEventId, int? dogProfileId)
        {
            _dogEventsRepo.Delete(dogEventId);

            return dogProfileId.HasValue
                       ? RedirectToAction("Index", new { dog = dogProfileId.Value })
                       : RedirectToAction("Index", "Dog");
        }
    }
}
