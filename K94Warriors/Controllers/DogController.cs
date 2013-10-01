using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K9ImageResizer = K94Warriors.Core.ImageResizing.ImageResizer;
using K94Warriors.Data.Contracts;
using K94Warriors.Core;
using K94Warriors.Models;
using K94Warriors.ViewModels;

namespace K94Warriors.Controllers
{
    [Authorize]
    public class DogController : BaseController
    {
        private readonly IRepository<DogProfile> _dogRepo;
        private readonly IRepository<DogImage> _dogImageRepo;
        private readonly IBlobRepository _blobRepo;

        public DogController(IRepository<DogProfile> dogRepo,
                             IRepository<DogImage> dogImageRepo,
                             IBlobRepository blobRepo)
        {
            if (dogRepo == null)
                throw new ArgumentNullException("dogRepo");
            _dogRepo = dogRepo;

            if (dogImageRepo == null)
                throw new ArgumentNullException("dogImageRepo");
            _dogImageRepo = dogImageRepo;

            if (blobRepo == null)
                throw new ArgumentNullException("blobRepo");
            _blobRepo = blobRepo;
        }


        //
        // GET: /Dog/Index/

        public ActionResult Index()
        {
            var dogs = _dogRepo.GetAll()
                .Include(d => d.Location).ToList();

            return View(dogs);
        }


        // 
        // GET: /Dog/DogProfile/{dogProfileId}

        public ActionResult DogProfile(int dogProfileId)
        {
            ViewBag.DogId = dogProfileId;
            var model = _dogRepo.GetAll()
                .Include(profile => profile.Location)
                .FirstOrDefault(profile => profile.ProfileID == dogProfileId);

            return View(new DogProfileViewModel(model));
        }


        // 
        // GET: /Dog/Create/

        [HttpGet]
        public ActionResult Create()
        {
            return View(new DogProfile());
        }


        //
        // POST: /Dog/Create/

        [HttpPost]
        public async Task<ActionResult> Create(DogProfile model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //model.CreatedByUserID = CurrentUserId;

            _dogRepo.Insert(model);

            return RedirectToAction("Index");
        }


        //
        // GET: /Dog/Edit/{dogProfileId}

        [HttpGet]
        public ActionResult Edit(int dogProfileId)
        {
            var model = _dogRepo.GetById(dogProfileId);

            if (model == null)
                return RedirectToAction("Index", "Home");

            SetDogViewBag(model);

            return View(model);
        }


        // 
        // POST: /Dog/Edit/

        [HttpPost]
        public async Task<ActionResult> Edit(DogProfile model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _dogRepo.Update(model);

            return RedirectToAction("Index");
        }


        //
        // POST: /Dog/Delete/{dogProfileId}

        [HttpPost]
        public ActionResult Delete(int dogProfileId)
        {
            _dogRepo.Delete(dogProfileId);
            return RedirectToAction("Index");
        }


        #region Partials and Child Actions

        public async Task<ActionResult> DogThumbnail(int dogId, int size)
        {
            var dogImage = _dogImageRepo
                                .GetAll()
                                .FirstOrDefault(i => i.DogProfileID == dogId);

            if (dogImage == null)
                return null;

            var imageStream = await _blobRepo.GetImageAsync<MemoryStream>(dogImage.BlobKey.ToString());
            var sizedImage = K9ImageResizer.ResizeToByteArray(imageStream, size, size, false);
            return new FileContentResult(sizedImage, dogImage.MimeType);
        }

        [ChildActionOnly]
        public ActionResult DogImagesPartial(int id)
        {
            var viewModel = _dogImageRepo.Where(x => x.DogProfileID == id).ToList();
            return PartialView("_DogImagesPartial", viewModel);
        }

        public async Task<ActionResult> ImageForBlobKey(string blobKey, string mimeType, int? height, int? width)
        {
            var image = await _blobRepo.GetImageAsync<MemoryStream>(blobKey);
            var resized = K9ImageResizer.ResizeToByteArray(image, 320, 180, false);
            return new FileContentResult(resized, mimeType);
        }

        public async Task<ActionResult> GetImage(string id)
        {
            var memoryStream = await _blobRepo.GetImageAsync<MemoryStream>(id);
            return File(memoryStream, "image/jpeg");
        }

        [ChildActionOnly]
        public ActionResult GetDogSection(int dogId)
        {
            var dog = _dogRepo.GetById(dogId);
            ViewBag.DogName = dog.Name;
            ViewBag.DogId = dog.ProfileID;

            return View("_DogSection");
        }

        public ActionResult ImageKeysForDogProfile(int dogProfileId)
        {
            var keys = _dogImageRepo
                .Where(image => image.DogProfileID == dogProfileId)
                .Select(image => new
                    {
                        image.BlobKey,
                        image.MimeType
                    });

            return Json(keys, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UploadDogImages(DogImageUploadViewModel model)
        {
            if (model.Files.Count < 1)
                return Json(new { success = false, message = "No files in request." });

            await UploadFiles(model.DogProfileId, model.Files);

            return Json(new {success = true});
        }

        [HttpPost]
        public ActionResult DeleteImage(string blobKey)
        {
            Guid key;
            var success = Guid.TryParse(blobKey, out key);
            if (!success)
                return Json(new { success = false, message = "Invalid blob key" });

            var image = _dogImageRepo.Where(i => i.BlobKey.Equals(key)).FirstOrDefault();
            if (image == null)
                return Json(new { success = false, message = "No image found for specified key" });

            _dogImageRepo.Delete(image);
            _blobRepo.DeleteImageIfExistsAsync(blobKey);
            return Json(new { success = true });
        }

        #endregion

        #region Private Methods

        private async Task UploadFiles(int dogProfileId, HttpFileCollectionBase fileCollection)
        {
            var fileList = new List<HttpPostedFileBase>();
            HttpPostedFileBase file;
            var enumerator = fileCollection.GetEnumerator();
            while (enumerator.MoveNext())
                if ((file = enumerator.Current as HttpPostedFileBase) != null)
                    fileList.Add(file);

            await UploadFiles(dogProfileId, fileList);
        }

        private async Task UploadFiles(int dogProfileId, IEnumerable<HttpPostedFileBase> files)
        {
            var dogImages = new List<DogImage>();
            foreach (var file in files)
            {
                var blobKey = Guid.NewGuid();
                var dogImage = new DogImage { BlobKey = blobKey, DogProfileID = dogProfileId, MimeType = file.ContentType };
                dogImages.Add(dogImage);
                await _blobRepo.InsertOrUpdateImageAsync(blobKey.ToString(), file.InputStream);
            }
            _dogImageRepo.Insert(dogImages);
        }

        #endregion
    }
}