using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public partial class MedicalRecordsController : BaseController
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogMedicalRecord> _dogMedicalRecordsRepo;
        private readonly IRepository<DogMedicalRecordImage> _dogMedicalRecordImageRepo;
        private readonly IRepository<MedicalRecordType> _medicalRecordTypesRepo;
        private readonly IBlobRepository _blobRepo;

        public MedicalRecordsController(IRepository<DogProfile> dogProfileRepo,
                                        IRepository<DogMedicalRecord> dogMedicalRecordsRepo,
                                        IRepository<DogMedicalRecordImage> dogMedicalRecordImageRepo,
                                        IRepository<MedicalRecordType> medicalRecordTypesRepo,
                                        IBlobRepository blobRepo)
        {
            _dogProfileRepo = dogProfileRepo;
            _dogMedicalRecordsRepo = dogMedicalRecordsRepo;
            _dogMedicalRecordImageRepo = dogMedicalRecordImageRepo;
            _medicalRecordTypesRepo = medicalRecordTypesRepo;
            _blobRepo = blobRepo;
        }


        //
        // GET: /MedicalRecords?dog={dogProfileId}

        public virtual ActionResult Index(DogProfile dog)
        {
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            SetDogViewBag(dog);

            var model = _dogMedicalRecordsRepo.Where(record => record.DogProfileID == dog.ProfileID).Include(i => i.MedicalRecordType).ToList();

            return View(model);
        }


        // 
        // GET: /MedicalRecords/Create?dog={dogProfileId}

        [HttpGet]
        public virtual ActionResult Create(DogProfile dog)
        {
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            ViewBag.MedicalRecordTypesSelectList = new SelectList(_medicalRecordTypesRepo.GetAll(), "MedicalRecordTypeID", "Name");

            SetDogViewBag(dog);

            return View(new DogMedicalRecord { DogProfileID = dog.ProfileID });
        }


        //
        // POST: /MedicalRecords/Create

        [HttpPost]
        public virtual async Task<ActionResult> Create(DogMedicalRecord model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.MedicalRecordTypesSelectList = new SelectList(_medicalRecordTypesRepo.GetAll(), "MedicalRecordTypeID", "Name");

                return View(model);
            }

            _dogMedicalRecordsRepo.Insert(model);

            if (files != null)
                await UploadFiles(model.RecordID, files);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }


        //
        // GET: /MedicalRecords/Edit

        public virtual ActionResult Edit(int dogMedicalRecordId)
        {
            var model = _dogMedicalRecordsRepo.GetById(dogMedicalRecordId);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            SetDogViewBag(dog);

            ViewBag.MedicalRecordTypesSelectList = new SelectList(_medicalRecordTypesRepo.GetAll(), "MedicalRecordTypeID", "Name");

            return View(model);
        }


        //
        // POST: /MedicalRecords/Edit

        [HttpPost]
        public virtual async Task<ActionResult> Edit(DogMedicalRecord model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.MedicalRecordTypesSelectList = new SelectList(_medicalRecordTypesRepo.GetAll(), "MedicalRecordTypeID", "Name");

                return View(model);
            }

            _dogMedicalRecordsRepo.Update(model);

            if (files != null)
                await UploadFiles(model.RecordID, files);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }


        // 
        // POST: /MedicalRecords/Delete

        [HttpPost]
        public virtual ActionResult Delete(int dogMedicalRecordId, int? dogProfileId)
        {
            _dogMedicalRecordsRepo.Delete(dogMedicalRecordId);
            return dogProfileId.HasValue
                       ? RedirectToAction("Index", new { dogProfileId = dogProfileId.Value })
                       : RedirectToAction("Index", "Dog");
        }


        private async Task UploadFiles(int dogMedicalRecordId, IEnumerable<HttpPostedFileBase> files)
        {
            var medicalRecordImages = new List<DogMedicalRecordImage>();
            foreach (var file in files)
            {
                var blobKey = Guid.NewGuid();
                var medicalRecordImage = new DogMedicalRecordImage
                {
                    BlobKey = blobKey,
                    DogMedicalRecordID = dogMedicalRecordId,
                    MimeType = file.ContentType
                };
                medicalRecordImages.Add(medicalRecordImage);
                await _blobRepo.InsertOrUpdateImageAsync(blobKey.ToString(), file.InputStream);
            }
            _dogMedicalRecordImageRepo.Insert(medicalRecordImages);
        }
    }
}
