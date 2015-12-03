using K94Warriors.Controllers;
using K94Warriors.Data.Contracts;
using K94Warriors.Filters;
using K94Warriors.Models;
using System.Linq;
using System.Web.Mvc;

namespace K94Warriors.Areas.Admin.Controllers
{
    [K9Authorize(Roles = "Admin")]
    public partial class NoteTypesController : BaseController
    {
        private readonly IRepository<NoteType> _noteTypesRepo;

        public NoteTypesController(IRepository<NoteType> noteTypesRepo)
        {
            _noteTypesRepo = noteTypesRepo;
        }

        public virtual ActionResult Index()
        {
            var model = _noteTypesRepo.GetAll().OrderBy(i => i.Name);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create() => View();

        [HttpPost]
        public virtual ActionResult Create(NoteType model)
        {
            var existingName = _noteTypesRepo.Where(i => i.Name == model.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Name) && existingName != null)
                ModelState.AddModelError("Name", "Name already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _noteTypesRepo.Insert(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var model = _noteTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(NoteType model)
        {
            var existingName = _noteTypesRepo.Where(i => i.Name == model.Name && i.ID != model.ID).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Name) && existingName != null)
                ModelState.AddModelError("Name", "Name already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _noteTypesRepo.Update(model);

            return RedirectToAction("Index");
        }

        public virtual ActionResult Delete(int id)
        {
            var model = _noteTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            _noteTypesRepo.Delete(model);

            return RedirectToAction("Index");
        }
    }
}