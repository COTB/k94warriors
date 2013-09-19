using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Data;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using K94Warriors.ViewModels;

namespace K94Warriors.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        private readonly IRepository<DogProfile> _dogRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<DogMedicalRecord> _recordRepo;
        private readonly IRepository<DogNote> _dogNoteRepo;
        private readonly IRepository<NoteType> _noteTypeRepo;
        private readonly IBlobRepository _blobRepo;
        private readonly IRepository<DogEvent> _dogEventRepo;
        private readonly IRepository<EventType> _dogEventTypeRepo;
        private readonly IRepository<DogSkill> _dogSkillRepo;

        public DogController(IRepository<DogProfile> dogRepo,
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
            var dogs = _dogRepo.GetAll();

            return View(dogs);
        }

        [HttpGet]
        public ActionResult CreateOrUpdateDog(int? id)
        {
            var viewModel = id.HasValue
                                ? DogProfileViewModel.FromDogProfile(_dogRepo.GetById(id.Value))
                                : new DogProfileViewModel();

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CreateOrUpdateDog(DogProfileViewModel viewModel, IEnumerable<HttpPostedFileBase> images)
        {
            var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (!user.IsUserAdminOrTrainer())
                return RedirectToAction("Error403", "Error"); ;

            DogProfile dogProfile;

            if (viewModel.ProfileID == 0)
            {
                dogProfile = viewModel.ToDogProfile();
                dogProfile.CreatedByUserID = user.UserID;
                _dogRepo.Insert(dogProfile);
            }
            else
            {
                dogProfile = viewModel.ToDogProfile(_dogRepo.GetById(viewModel.ProfileID));
                _dogRepo.Update(dogProfile);
            }

            // Handle upload of images to blob storage
            foreach (var image in images)
            {
                // TODO: verify correct file type for security?
                // TODO: no idea how we want to name these blobs...
                _blobRepo.InsertOrUpdateImageAsync(dogProfile.ProfileID + "_" + image.FileName, image.InputStream);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ReadDog(int id)
        {
            ViewBag.DogId = id;
            var dog = _dogRepo.GetById(id);

            return View(DogProfileViewModel.FromDogProfile(dog));

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
                return RedirectToAction("ReadDog", new { id = dogEvent.DogProfileID });
            }

            return RedirectToAction("Error403", "Error");
        }


        [HttpGet]
        public ActionResult CreateOrUpdateDogEvent(int dogId, int? eventId)
        {
            var dog = _dogRepo.GetById(dogId);

            var model = eventId.HasValue ? _dogEventRepo.GetById(eventId.Value) : new DogEvent { DogProfileID = dogId };

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
        public ActionResult GetDogsection(int dogId)
        {
            var dog = _dogRepo.GetById(dogId);
            ViewBag.DogName = dog.Name;
            ViewBag.DogId = dog.ProfileID;

            return View("_DogSection");
        }
    }
}