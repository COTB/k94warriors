using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogMedicalRecord> _dogMedicalRecordsRepo;
        private readonly IRepository<DogMedicalRecordImage> _dogMedicalRecordImageRepo;
        private readonly IBlobRepository _blobRepo;

        public MedicalRecordsController(IRepository<DogProfile> dogProfileRepo,
                                        IRepository<DogMedicalRecord> dogMedicalRecordsRepo,
                                        IRepository<DogMedicalRecordImage> dogMedicalRecordImageRepo,
                                        IBlobRepository blobRepo)
        {
            if (dogProfileRepo == null)
                throw new ArgumentNullException("dogProfileRepo");
            _dogProfileRepo = dogProfileRepo;

            if (dogMedicalRecordsRepo == null)
                throw new ArgumentNullException("dogMedicalRecordsRepo");
            _dogMedicalRecordsRepo = dogMedicalRecordsRepo;

            if (dogMedicalRecordImageRepo == null)
                throw new ArgumentNullException("dogMedicalRecordImageRepo");
            _dogMedicalRecordImageRepo = dogMedicalRecordImageRepo;

            if (blobRepo == null)
                throw new ArgumentNullException("blobRepo");
            _blobRepo = blobRepo;
        }


        //
        // GET: /MedicalRecords/

        public ActionResult Index(int dogProfileId)
        {
            var dog = _dogProfileRepo.GetById(dogProfileId);
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;
            var model = _dogMedicalRecordsRepo
                .Where(record => record.DogProfileID == dogProfileId);
            return View(model);
        }


        // 
        // GET: /MedicalRecords/Create/

        public ActionResult Create(int dogProfileId)
        {
            var dog = _dogProfileRepo.GetById(dogProfileId);
            if (dog == null)
                return RedirectToAction("Index", "Dog");

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;

            return View(new DogMedicalRecord { DogProfileID = dogProfileId });
        }


        //
        // POST: /MedicalRecords/Create

        [HttpPost]
        public async Task<ActionResult> Create(DogMedicalRecord model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);
                ViewBag.DogId = model.DogProfileID;
                ViewBag.DogName = dog.Name;
                return View(model);
            }

            _dogMedicalRecordsRepo.Insert(model);

            if (files != null)
                await UploadFiles(model.RecordID, files);

            return RedirectToAction("Index", new { dogProfileId = model.DogProfileID });
        }


        //
        // GET: /MedicalRecords/Edit

        public ActionResult Edit(int dogMedicalRecordId)
        {
            var model = _dogMedicalRecordsRepo.GetById(dogMedicalRecordId);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = _dogProfileRepo.GetById(model.DogProfileID);

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;

            return View(model);
        }


        //
        // POST: /MedicalRecords/Edit

        [HttpPost]
        public async Task<ActionResult> Edit(DogMedicalRecord model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);
                ViewBag.DogId = model.DogProfileID;
                ViewBag.DogName = dog.Name;
                return View(model);
            }

            _dogMedicalRecordsRepo.Update(model);

            if (files != null)
                await UploadFiles(model.RecordID, files);

            return RedirectToAction("Index", new { dogProfileId = model.DogProfileID });
        }


        // 
        // POST: /MedicalRecords/Delete

        [HttpPost]
        public ActionResult Delete(int dogMedicalRecordId, int? dogProfileId)
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
