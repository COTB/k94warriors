using K94Warriors.Controllers;
using K94Warriors.Data.Contracts;
using K94Warriors.Filters;
using K94Warriors.Models;
using System.Linq;
using System.Web.Mvc;

namespace K94Warriors.Areas.Admin.Controllers
{
    [K9Authorize(Roles = "Admin")]
    public partial class MedicalRecordTypesController : BaseController
    {
        private readonly IRepository<MedicalRecordType> _medicalRecordTypesRepo;

        public MedicalRecordTypesController(IRepository<MedicalRecordType> medicalRecordTypesRepo)
        {
            _medicalRecordTypesRepo = medicalRecordTypesRepo;
        }

        public virtual ActionResult Index()
        {
            var model = _medicalRecordTypesRepo.GetAll().OrderBy(i => i.Name);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(MedicalRecordType model)
        {
            var existingName = _medicalRecordTypesRepo.Where(i => i.Name == model.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Name) && existingName != null)
                ModelState.AddModelError("Name", "Name already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _medicalRecordTypesRepo.Insert(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var model = _medicalRecordTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(MedicalRecordType model)
        {
            var existingName = _medicalRecordTypesRepo.Where(i => i.Name == model.Name && i.MedicalRecordTypeID != model.MedicalRecordTypeID).FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Name) && existingName != null)
                ModelState.AddModelError("Name", "Name already exists, please try another.");

            if (!ModelState.IsValid)
                return View(model);

            _medicalRecordTypesRepo.Update(model);

            return RedirectToAction("Index");
        }

        public virtual ActionResult Delete(int id)
        {
            var model = _medicalRecordTypesRepo.GetById(id);

            if (model == null)
                return HttpNotFound();

            _medicalRecordTypesRepo.Delete(model);

            return RedirectToAction("Index");
        }
    }
}
