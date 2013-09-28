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

            var meds = _dogMedicationRepo.Where(i => i.DogProfileID == dog.ProfileID).ToList();

            SetDogViewBag(dog);

            return View(meds);
        }

        public ActionResult PrintLog(int dogProfileId, int? numdays, DateTime? startdate)
        {
            var dog = _dogProfileRepo.GetById(dogProfileId);

            if (dog == null)
                return HttpNotFound();

            var meds = _dogMedicationRepo.Where(i => i.DogProfileID == dog.ProfileID).ToList();

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
    }
}
