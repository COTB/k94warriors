using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using K94Warriors.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Controllers
{
    public class PrintController : BaseController
    {
        private readonly IRepository<DogProfile> _dogRepo;

        public PrintController(IRepository<DogProfile> dogRepo)
        {
            _dogRepo = dogRepo;
        }

        public new ActionResult Profile(int id)
        {
            var profile = _dogRepo.GetById(id);

            if (profile == null)
                return HttpNotFound();

            var vaccines = profile.DogMedicalRecords
                .Where(i => i.MedicalRecordType.Name.Contains("Vaccin")) // HACK: matches "Vaccine" or "Vaccination"
                .OrderByDescending(i => i.RecordExpirationDate)
                .ToList();

            var notes = profile.DogNotes
                .OrderByDescending(i => i.CreatedDate)
                .ToList();

            var feeding = profile.DogFeedingEntries.ToList();

            var images = profile.Images.ToList();

            var vm = new DogPrintViewModel();
            vm.DogProfile = profile;
            vm.Vaccinations = vaccines;
            vm.Notes = notes;
            vm.Feeding = feeding;
            vm.Images = images;
            
            return View(vm);
        }

    }
}
