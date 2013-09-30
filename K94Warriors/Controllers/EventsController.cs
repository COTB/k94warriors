using System;
using System.Linq;
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
            var model = _dogEventsRepo.Where(d => d.DogProfileID == dogProfileId).ToList();

            if (!model.Any())
                return RedirectToAction("DogProfile", "Dog", new {id = dogProfileId});

            return View(model);
        }


        //
        // GET: /Events/Create/{dogProfileId}

        public ActionResult Create(int dogProfileId)
        {
            // verify dog exists
            var dog = _dogProfileRepo.GetById(dogProfileId);
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;

            return View(new DogEvent { DogProfileID = dogProfileId });
        }


        //
        // POST: /Events/Create/

        [HttpPost]
        public ActionResult Create(DogEvent model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _dogEventsRepo.Insert(model);

            return RedirectToAction("Index", new {dogProfileId = model.DogProfileID});
        }


        //
        // GET: /Events/Edit/{dogEventId}

        public ActionResult Edit(int dogEventId)
        {
            var model = _dogEventsRepo.GetById(dogEventId);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;

            return View(model);
        }


        // 
        // POST: /Events/Edit/

        [HttpPost]
        public ActionResult Edit(DogEvent model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _dogEventsRepo.Update(model);

            return RedirectToAction("Index", new { dogProfileId = model.DogProfileID });
        }


        // 
        // POST: /Events/Delete/{dogEventId}

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
