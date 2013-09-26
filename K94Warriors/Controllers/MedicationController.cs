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
    public class MedicationController : Controller
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogMedication> _dogMedicationRepo;

        public MedicationController(IRepository<DogProfile> dogProfileRepo, IRepository<DogMedication> dogMedicationRepo)
        {
            _dogProfileRepo = dogProfileRepo;
            _dogMedicationRepo = dogMedicationRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintLog(int dogProfileId)
        {
            var dog = _dogProfileRepo.GetById(dogProfileId);

            if (dog == null)
                return HttpNotFound();

            var meds = _dogMedicationRepo.Where(i => i.DogProfileID == dog.ProfileID).ToList();

            var viewModel = new MedicationPrintLogViewModel();
            viewModel.DogProfile = dog;
            viewModel.Medications = meds;

            return View(viewModel);
        }
    }
}
