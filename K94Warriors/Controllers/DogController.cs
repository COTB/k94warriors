using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Core.ImageResizing;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using K94Warriors.ViewModels;

namespace K94Warriors.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        private readonly IRepository<DogProfile> _dogRepo;
        private readonly IRepository<DogImage> _dogImageRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IBlobRepository _blobRepo;

        public DogController(IRepository<DogProfile> dogRepo,
                                IRepository<DogImage> dogImageRepo,
                                IRepository<User> userRepo,
                                IBlobRepository blobRepo)
        {
            if (dogRepo == null)
                throw new ArgumentNullException("dogRepo");
            _dogRepo = dogRepo;

            if (dogImageRepo == null)
                throw new ArgumentNullException("dogImageRepo");
            _dogImageRepo = dogImageRepo;

            if (userRepo == null)
                throw new ArgumentNullException("userRepo");
            _userRepo = userRepo;

            if (blobRepo == null)
                throw new ArgumentNullException("blobRepo");
            _blobRepo = blobRepo;
        }

        public ActionResult Index()
        {
            var dogs = _dogRepo.GetAll()
                .Include(d => d.Location).ToList();

            return View(dogs);
        }

        public async Task<ActionResult> DogThumbnail(int dogId, int size)
        {
            var dogImage = _dogImageRepo
                                .GetAll()
                                .FirstOrDefault(i => i.DogProfileID == dogId);

            if (dogImage == null)
                return null;

            var imageStream = await _blobRepo.GetImageAsync<MemoryStream>(dogImage.BlobKey.ToString());
            var sizedImage = ImageResizer.ResizeToByteArray(imageStream, size, size, false);
            return new FileContentResult(sizedImage, dogImage.MimeType);
        }

        [HttpGet]
        public ActionResult CreateOrUpdateDog(int? id)
        {
            var viewModel = id.HasValue
                                ? _dogRepo.GetById(id.Value)
                                : new DogProfile { PickedUpDate = DateTime.UtcNow };

            return View(viewModel);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new DogProfile());
        }


        [HttpPost]
        public ActionResult Create(DogProfile model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            model.CreatedByUserID = user.UserID;

            _dogRepo.Insert(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int dogProfileId)
        {
            var model = _dogRepo.GetById(dogProfileId);

            if (model == null)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DogProfile model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _dogRepo.Update(model);

            return RedirectToAction("Index");
        }

        public ActionResult DogProfile(int id)
        {
            ViewBag.DogId = id;
            var dog = _dogRepo.GetById(id);

            return View(DogProfileViewModel.FromDogProfile(dog));
        }

        [ChildActionOnly]
        public ActionResult DogImagesPartial(int id)
        {
            var viewModel = _dogImageRepo.Where(x => x.DogProfileID == id).ToList();
            return PartialView("_DogImagesPartial", viewModel);
        }

        public async Task<ActionResult> ImageForBlobKey(string blobKey, string mimeType, int height, int width)
        {
            var image = await _blobRepo.GetImageAsync<MemoryStream>(blobKey);
            var resized = ImageResizer.ResizeToByteArray(image, 320, 180, false);
            return new FileContentResult(resized, mimeType);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dog = _dogRepo.GetById(id);
            return View(dog);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            _dogRepo.Delete(id);
            return RedirectToAction("Index");
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
    }
}