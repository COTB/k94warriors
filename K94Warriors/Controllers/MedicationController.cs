using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using K94Warriors.ViewModels.Medication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Controllers
{
    public class MedicationController : BaseController
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogMedication> _dogMedicationRepo;

        public MedicationController(IRepository<DogProfile> dogProfileRepo, IRepository<DogMedication> dogMedicationRepo)
        {
            _dogProfileRepo = dogProfileRepo;
            _dogMedicationRepo = dogMedicationRepo;
        }

        public ActionResult Index(DogProfile dog)
        {
            if (dog == null)
                return HttpNotFound();

            var meds = _dogMedicationRepo.Where(i => i.DogProfileID == dog.ProfileID).OrderByDescending(i => i.EndDate).ToList();

            SetDogViewBag(dog);

            return View(meds);
        }

        public ActionResult PrintLog(int dogProfileId, int? numdays, DateTime? startdate)
        {
            var dog = _dogProfileRepo.GetById(dogProfileId);

            if (dog == null)
                return HttpNotFound();

            var meds = _dogMedicationRepo.Where(i => i.DogProfileID == dog.ProfileID).OrderByDescending(i => i.EndDate).ToList();

            if (!numdays.HasValue)
                numdays = 10;
            if (!startdate.HasValue)
                startdate = DateTime.Now;

            var days = new List<DateTime>();
            var date = startdate.Value.AddDays(-1);
            
            for (int i = 0; i < numdays; i++)
            {
                days.Add((date = date.AddDays(1)));
            }

            var viewModel = new MedicationPrintLogViewModel();
            viewModel.DogProfile = dog;
            viewModel.Medications = meds;
            viewModel.NumDays = numdays.Value;
            viewModel.StartDate = startdate.Value;
            viewModel.Days = days;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create(DogProfile dog)
        {
            if (dog == null)
                return HttpNotFound();

            SetDogViewBag(dog);

            return View();
        }

        [HttpPost]
        public ActionResult Create(DogMedication model)
        {
            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            if (dog == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                SetDogViewBag(dog);
                return View(model);
            }

            _dogMedicationRepo.Insert(model);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var med = _dogMedicationRepo.GetById(id);

            if (med == null)
                return HttpNotFound();

            var dog = _dogProfileRepo.GetById(med.DogProfileID);

            if (dog == null)
                return HttpNotFound();

            SetDogViewBag(dog);

            return View(med);
        }

        [HttpPost]
        public ActionResult Edit(DogMedication med)
        {
            if (med == null)
                return HttpNotFound();

            var dog = _dogProfileRepo.GetById(med.DogProfileID);

            if (dog == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                SetDogViewBag(dog);
                return View(med);
            }

            _dogMedicationRepo.Update(med);

            return RedirectToAction("Index", new { dog = dog.ProfileID });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var med = _dogMedicationRepo.GetById(id);

            if (med == null)
                return HttpNotFound();

            _dogMedicationRepo.Delete(id);

            return RedirectToAction("Index", new { dog = med.DogProfileID });
        }
    }
}
