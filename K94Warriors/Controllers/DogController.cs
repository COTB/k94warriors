using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using K94Warriors.Data;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class DogController : Controller
    {
        private readonly IRepository<DogProfile> _dogRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<DogMedicalRecord> _recordRepo;
        private readonly IRepository<DogNote> _dogNoteRepo;
        private readonly IRepository<NoteType> _noteTypeRepo;
        private readonly IBlobRepository _blobRepo;

        public DogController(IRepository<DogProfile> dogRepo,
                                IRepository<User> userRepo,
                                IRepository<DogMedicalRecord> recordRepo,
                                IRepository<DogNote> dogNoteRepo,
                                IRepository<NoteType> noteTypeRepo,
                                IBlobRepository blobRepo)
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
        }

        public ActionResult Index()
        {
            var dogs = _dogRepo.GetAll();

            return View(dogs);
        }

        [HttpGet]
        public ActionResult CreateOrUpdateDog(int? id)
        {
            DogProfile viewModel;

            viewModel = id.HasValue ? _dogRepo.GetById(id.Value) : new DogProfile();

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateOrUpdateDog(DogProfile dogProfile)
        {
            var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (user.UserType.Name != "ADMIN" && user.UserType.Name != "TRAINER")
                return RedirectToAction("Index");

            if (dogProfile.ProfileID == 0)
            {
                dogProfile.CreatedByUserID = user.UserID;
                _dogRepo.Insert(dogProfile);
            }
            else
            {
                _dogRepo.Update(dogProfile);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ReadDog(int id)
        {
            return View(_dogRepo.GetById(id));
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

        public ActionResult GetDocuments(int id)
        {
            var documents = _recordRepo.GetAll().Where(d => d.DogProfileID == id);

            return Json(documents.ToList(), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetDocument(string id)
        {
            var memoryStream = await _blobRepo.GetImageAsync<MemoryStream>("images", id);
            return File(memoryStream, "image/jpeg");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateOrUpdateDogNote(DogNote dogNote)
        {
            if (dogNote.NoteID == 0)
            {
                var user = _userRepo.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

                dogNote.CreatedDate = DateTime.UtcNow;
                dogNote.CreatedByUserId = user.UserID;

                _dogNoteRepo.Insert(dogNote);
            }
            return RedirectToAction("ReadDog", new { id = dogNote.DogProfileID });
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateOrUpdateDogNote(int dogId, int? noteId)
        {
            DogNote viewModel;
            var dog = _dogRepo.GetById(dogId);

            viewModel = noteId.HasValue ? _dogNoteRepo.GetById(noteId.Value) : new DogNote { DogProfileID = dogId };

            ViewBag.NoteTypeId = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", viewModel.NoteTypeId);
            ViewBag.DogName = dog.Name;
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

            ViewBag.DogName = dog.Name;
            ViewBag.DogId = dog.ProfileID;

            return View(model);
        }

        public ActionResult DeleteDogNote(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult CreateOrUpdateDogEvent(DogEvent dogEvent)
        {
            throw new NotImplementedException();
        }

        public ActionResult ReadDogEvent(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteDogEvent(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult CreateOrUpdateDogSkill(DogSkill dogSkill)
        {
            throw new NotImplementedException();
        }

        public ActionResult ReadDogSkill(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteDogSkill(int id)
        {
            throw new NotImplementedException();
        }
    }
}