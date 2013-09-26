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
        private readonly IRepository<DogMedicalRecord> _recordRepo;
        private readonly IRepository<DogNote> _dogNoteRepo;
        private readonly IRepository<NoteType> _noteTypeRepo;
        private readonly IBlobRepository _blobRepo;
        private readonly IRepository<DogEvent> _dogEventRepo;
        private readonly IRepository<EventType> _dogEventTypeRepo;
        private readonly IRepository<DogSkill> _dogSkillRepo;

        public DogController(IRepository<DogProfile> dogRepo,
                                IRepository<DogImage> dogImageRepo,
                                IRepository<User> userRepo,
                                IRepository<DogMedicalRecord> recordRepo,
                                IRepository<DogNote> dogNoteRepo,
                                IRepository<NoteType> noteTypeRepo,
                                IBlobRepository blobRepo,
                                IRepository<DogEvent> dogEventRepo,
                                IRepository<EventType> dogEventTypeRepo,
                                IRepository<DogSkill> dogSkillRepo)
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

            if (recordRepo == null)
                throw new ArgumentNullException("recordRepo");
            _recordRepo = recordRepo;

            if (dogNoteRepo == null)
                throw new ArgumentNullException("dogNoteRepo");
            _dogNoteRepo = dogNoteRepo;

            if (noteTypeRepo == null)
                throw new ArgumentNullException("noteTypeRepo");
            _noteTypeRepo = noteTypeRepo;

            if (blobRepo == null)
                throw new ArgumentNullException("blobRepo");
            _blobRepo = blobRepo;
            _dogEventRepo = dogEventRepo;
            _dogEventTypeRepo = dogEventTypeRepo;
            _dogSkillRepo = dogSkillRepo;
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


        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateDog(DogProfile dogProfile, IEnumerable<HttpPostedFileBase> images)
        {
            var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (!user.IsUserAdminOrTrainer())
                return RedirectToAction("Error403", "Error");


            if (dogProfile.ProfileID == 0)
            {
                dogProfile.CreatedByUserID = user.UserID;
                _dogRepo.Insert(dogProfile);
            }
            else
            {
                _dogRepo.Update(dogProfile);
            }

            try
            {
                await UploadFiles(dogProfile.ProfileID, images);
            }
            catch (Exception ex)
            {
                return Json(new {ex.Message, ex.StackTrace}, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ReadDog(int id)
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
        public ActionResult DeleteDog(int id)
        {
            var dog = _dogRepo.GetById(id);
            return View(dog);
        }

        [HttpPost]
        public ActionResult DeleteDog(int id, FormCollection formCollection)
        {
            _dogRepo.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetMedicalRecords(int id)
        {
            var documents = _recordRepo.GetAll().Where(d => d.DogProfileID == id);

            ViewBag.DogId = id;
            return View(documents);
        }

        public async Task<ActionResult> GetImage(string id)
        {
            var memoryStream = await _blobRepo.GetImageAsync<MemoryStream>(id);
            return File(memoryStream, "image/jpeg");
        }


        [HttpPost]
        public ActionResult CreateOrUpdateDogNote(DogNote dogNote)
        {
            var user = _userRepo.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

            if (dogNote.NoteID == 0)
            {
                dogNote.CreatedDate = DateTime.UtcNow;
                dogNote.CreatedByUserId = user.UserID;

                _dogNoteRepo.Insert(dogNote);
            }
            else
            {
                // No update columns on note. How to specify what user edited a note and when? Overwriting for now with last edit wins.
                dogNote.CreatedByUserId = user.UserID;
                dogNote.CreatedDate = DateTime.UtcNow;
                _dogNoteRepo.Update(dogNote);
            }
            return RedirectToAction("ReadDog", new { id = dogNote.DogProfileID });

        }


        [HttpGet]
        public ActionResult CreateOrUpdateDogNote(int dogId, int? noteId)
        {
            var dog = _dogRepo.GetById(dogId);
            var viewModel = noteId.HasValue ? _dogNoteRepo.GetById(noteId.Value) : new DogNote { DogProfileID = dogId };
            ViewBag.NoteTypeId = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", viewModel.NoteTypeId);
            ViewBag.DogId = dog.ProfileID;

            return View(viewModel);
        }

        public ActionResult GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetNotes(int dogId)
        {
            var model = _dogNoteRepo.Where(n => n.DogProfileID == dogId);
            var dog = _dogRepo.GetById(dogId);

            ViewBag.DogId = dog.ProfileID;

            return View(model);
        }

        public ActionResult DeleteDogNote(int id)
        {
            _dogNoteRepo.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult CreateOrUpdateDogEvent(DogEvent dogEvent)
        {

            var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (user != null && user.IsUserAdminOrTrainer())
            {

                if (dogEvent.EventID == 0)
                {
                    _dogEventRepo.Insert(dogEvent);
                }
                else
                {
                    _dogEventRepo.Update(dogEvent);
                }
                return RedirectToAction("GetDogEvents", new { dogId = dogEvent.DogProfileID });
            }

            return RedirectToAction("Error403", "Error");
        }


        [HttpGet]
        public ActionResult CreateOrUpdateDogEvent(int dogId, int? eventId)
        {
            var dog = _dogRepo.GetById(dogId);

            var model = eventId.HasValue ? _dogEventRepo.GetById(eventId.Value) : new DogEvent { DogProfileID = dogId, EventDate = DateTime.UtcNow };

            ViewBag.EventTypeId = new SelectList(_dogEventTypeRepo.GetAll(), "ID", "Name", model.EventTypeId);
            ViewBag.DogId = dog.ProfileID;

            return View(model);
        }

        [HttpGet]
        public ActionResult GetDogEvent(int dogId, int id)
        {
            ViewBag.DogId = dogId;
            return View(_dogEventRepo.GetById(id));
        }

        [HttpGet]
        public ActionResult GetDogEvents(int dogId)
        {

            ViewBag.DogId = dogId;
            return View(_dogEventRepo.Where(d => d.DogProfileID == dogId));
        }

        public ActionResult DeleteDogEvent(int id)
        {

            _dogEventRepo.Delete(id);
            return RedirectToAction("ReadDog", new { id = _dogEventRepo.GetById(id).DogProfileID });
        }

        [HttpPost]
        public ActionResult CreateOrUpdateDogSkill(DogSkill dogSkill)
        {

            var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (user != null && user.IsUserAdminOrTrainer())
            {

                if (dogSkill.DogSkilID == 0)
                {
                    _dogSkillRepo.Insert(dogSkill);
                }
                else
                {
                    _dogSkillRepo.Update(dogSkill);
                }
                return RedirectToAction("ReadDog", new { id = dogSkill.DogProfileID });
            }

            return RedirectToAction("Error403", "Error");
        }

        public ActionResult CreateOrUpdateDogSkill(int dogId, int? dogskillId)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetDogSkill(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteDogSkill(int id)
        {

            _dogSkillRepo.Delete(id);
            return RedirectToAction("ReadDog", new { id = _dogSkillRepo.GetById(id).DogProfileID });
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
                // TODO: verify correct file type?
                var blobKey = Guid.NewGuid();
                var dogImage = new DogImage { BlobKey = blobKey, DogProfileID = dogProfileId, MimeType = file.ContentType };
                dogImages.Add(dogImage);
                await _blobRepo.InsertOrUpdateImageAsync(blobKey.ToString(), file.InputStream);
            }
            _dogImageRepo.Insert(dogImages);
        }
    }
}