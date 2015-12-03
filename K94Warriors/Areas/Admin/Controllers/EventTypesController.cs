using K94Warriors.Controllers;
using K94Warriors.Data.Contracts;
using K94Warriors.Filters;
using K94Warriors.Models;
using System.Linq;
using System.Web.Mvc;

namespace K94Warriors.Areas.Admin.Controllers
{
    [K9Authorize(Roles = "Admin")]
    public partial class EventTypesController : BaseController
    {
        private readonly IRepository<EventType> _eventTypesRepo;

        public EventTypesController(IRepository<EventType> eventTypesRepo)
        {
            _eventTypesRepo = eventTypesRepo;
        }

        public virtual ActionResult Index()
        {
            var model = _eventTypesRepo.GetAll().OrderBy(i => i.Name);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create() => View();

        [HttpPost]
        public virtual ActionResult Create(EventType model)
        {
            var existingName = _eventTypesRepo.Where(i => i.Name == model.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Name) && existingName != null)
                ModelState.AddModelError("Name", "Name already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _eventTypesRepo.Insert(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var model = _eventTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(EventType model)
        {
            var existingName = _eventTypesRepo.Where(i => i.Name == model.Name && i.ID != model.ID).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Name) && existingName != null)
                ModelState.AddModelError("Name", "Name already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _eventTypesRepo.Update(model);

            return RedirectToAction("Index");
        }

        public virtual ActionResult Delete(int id)
        {
            var model = _eventTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            _eventTypesRepo.Delete(model);

            return RedirectToAction("Index");
        }
    }
}
