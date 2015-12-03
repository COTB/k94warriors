using System;
using System.Linq;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using K94Warriors.Helpers;

namespace K94Warriors.Controllers
{
    public partial class EventsController : BaseController
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

        public virtual ActionResult Index(DogProfile dog)
        {
            // verify dog exists
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            var model = _dogEventsRepo.Where(d => d.DogProfileID == dog.ProfileID).OrderByDescending(i => i.EventDate).ToList();

            SetDogViewBag(dog);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create(DogProfile dog)
        // GET: /Events/Create/{dogProfileId}

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


        //
        // POST: /Events/Create/

        [HttpPost]
        public virtual ActionResult Create(DogEvent model, string eventTime)
        {
            var timespan = TimeParserHelper.Parse(eventTime);

            if (!timespan.HasValue)
            {
                // this will trigger ModelState being invalid below
                ModelState.AddModelError("EventTime", "Please enter a valid time.");
            }

            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", null);

                return View(model);
            }

            model.EventDate = model.EventDate.Add(timespan.GetValueOrDefault());

            _dogEventsRepo.Insert(model);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }


        //
        // GET: /Events/Edit/{dogEventId}

        public virtual ActionResult Edit(int dogEventId)
        {
            var model = _dogEventsRepo.GetById(dogEventId);

            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            SetDogViewBag(dog);

            ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", model.EventTypeId);

            return View(model);
        }


        // 
        // POST: /Events/Edit/

        [HttpPost]
        public virtual ActionResult Edit(DogEvent model, string eventTime)
        {
            var timespan = TimeParserHelper.Parse(eventTime);

            if (!timespan.HasValue)
            {
                // this will trigger ModelState being invalid below
                ModelState.AddModelError("EventTime", "Please enter a valid time.");
            }

            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.EventTypeSelectList = new SelectList(_eventTypesRepo.GetAll(), "ID", "Name", null);

                return View(model);
            }

            model.EventDate = model.EventDate.Add(timespan.GetValueOrDefault());

            _dogEventsRepo.Update(model);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }


        // 
        // POST: /Events/Delete/{dogEventId}

        [HttpPost]
        public virtual ActionResult Delete(int dogEventId, int? dogProfileId)
        {
            _dogEventsRepo.Delete(dogEventId);

            return dogProfileId.HasValue
                       ? RedirectToAction("Index", new { dog = dogProfileId.Value })
                       : RedirectToAction("Index", "Dog");
        }
    }
}
