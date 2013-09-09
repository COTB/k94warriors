using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using K94Warriors.Enums;
using K94Warriors.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using System.IO;


namespace K94Warriors.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        IRepository<DogProfile> _dogRepo;

        public DogController()
        {
            _dogRepo = RepoResolver.GetRepository<DogProfile>();
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


        [HttpPost]
        public ActionResult CreateOrUpdateDog(DogProfile dogProfile)
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            var userRep = RepoResolver.GetRepository<User>();
            var user = userRep.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

            if (!user.IsUserAdminOrTrainer())
                return RedirectToAction("Error403", "Error");;

            if (dogProfile.ProfileID == 0)
            {
                dogProfile.CreatedByUserID = user.UserID;
                repo.Insert(dogProfile);

            }
            else
            {
                repo.Update(dogProfile);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ReadDog(int id)
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            ViewBag.DogId = id;
            return View(repo.GetById(id));
        }

        [HttpGet]
        public ActionResult DeleteDog(int id)
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            var dog = repo.GetById(id);
            return View(dog);
        }

        [HttpPost]
        public ActionResult DeleteDog(int id, FormCollection formCollection)
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetDocuments(int id)
        {
            var repo = RepoResolver.GetRepository<DogMedicalRecord>();
            var documents = repo.GetAll().Where(d => d.DogProfileID == id);

            ViewBag.DogId = id;
            return View(documents);
        }

        public ActionResult GetDocument(string id)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageAccountConnectionString"));
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("images");
            var blockBlob = container.GetBlockBlobReference(id);

            var memoryStream = new MemoryStream();
        
            blockBlob.DownloadToStream(memoryStream);
            memoryStream.Position = 0;
            return File(memoryStream, "image/jpeg");
           
        }


        [HttpPost]
        public ActionResult CreateOrUpdateDogNote(DogNote dogNote)
        {
            var repo = RepoResolver.GetRepository<DogNote>();
            var userRepo = RepoResolver.GetRepository<User>();
            var user = userRepo.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();


            if (dogNote.NoteID == 0)
            {
                dogNote.CreatedDate = DateTime.UtcNow;
                dogNote.CreatedByUserId = user.UserID;

                repo.Insert(dogNote);
            }
            else
            {
                // No update columns on note. How to specify what user edited a note and when? Overwriting for now with last edit wins.
                dogNote.CreatedByUserId = user.UserID;
                dogNote.CreatedDate = DateTime.UtcNow;
                repo.Update(dogNote);
            }
            return RedirectToAction("ReadDog", new {id = dogNote.DogProfileID});
        }


        [HttpGet]
        public ActionResult CreateOrUpdateDogNote(int dogId, int? noteId)
        {
            DogNote viewModel;
            var dog = _dogRepo.GetById(dogId);
            var repo = RepoResolver.GetRepository<DogNote>();
            var noteTypeRepo = RepoResolver.GetRepository<NoteType>();

            viewModel = noteId.HasValue ? repo.GetById(noteId.Value) : new DogNote {DogProfileID = dogId};

            ViewBag.NoteTypeId = new SelectList(noteTypeRepo.GetAll(), "ID", "Name", viewModel.NoteTypeId);

            ViewBag.DogId = dog.ProfileID;

            return View(viewModel);
        }

        public ActionResult GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetNotes(int dogId)
        {
            var repo = RepoResolver.GetRepository<DogNote>();
            var model = repo.Where(n => n.DogProfileID == dogId);
            var dog = _dogRepo.GetById(dogId);

            ViewBag.DogId = dog.ProfileID;

            return View(model);
        }

        public ActionResult DeleteDogNote(int id)
        {
            var repo = RepoResolver.GetRepository<DogNote>();
            repo.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult CreateOrUpdateDogEvent(DogEvent dogEvent)
        {                
            
            var userRepo = RepoResolver.GetRepository<User>();
            var user = userRepo.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

            if (user.IsUserAdminOrTrainer())
            {
                var repo = RepoResolver.GetRepository<DogEvent>();

                if (dogEvent.EventID == 0)
                {
                    repo.Insert(dogEvent);
                }
                else
                {
                    repo.Update(dogEvent);
                }
                return RedirectToAction("ReadDog", new { id = dogEvent.DogProfileID });
            }

            return RedirectToAction("Error403", "Error");
        }


        [HttpGet]
        public ActionResult CreateOrUpdateDogEvent(int dogId, int? eventId)
        {
            DogEvent model;
            var dog = _dogRepo.GetById(dogId);
            var repo = RepoResolver.GetRepository<DogEvent>();
            var eventTypeRepo = RepoResolver.GetRepository<EventType>();

            model = eventId.HasValue ? repo.GetById(eventId.Value) : new DogEvent { DogProfileID = dogId };

            ViewBag.NoteTypeId = new SelectList(eventTypeRepo.GetAll(), "ID", "Name", model.EventTypeId);
            ViewBag.DogId = dog.ProfileID;

            return View(model);
        }

        [HttpGet]
        public ActionResult GetDogEvent(int dogId, int id)
        {
            ViewBag.DogId = dogId;
            var repo = RepoResolver.GetRepository<DogEvent>();
            return View(repo.GetById(id));
        }

        [HttpGet]
        public ActionResult GetDogEvents(int dogId)
        {
            var repo = RepoResolver.GetRepository<DogEvent>();
            ViewBag.DogId = dogId;
            return View(repo.Where(d => d.DogProfileID == dogId));
        }

        public ActionResult DeleteDogEvent(int id)
        {
            var repo = RepoResolver.GetRepository<DogEvent>();
            repo.Delete(id);
            return RedirectToAction("ReadDog", new { id = repo.GetById(id).DogProfileID });
        }

        [HttpPost]
        public ActionResult CreateOrUpdateDogSkill(DogSkill dogSkill)
        {
            var userRepo = RepoResolver.GetRepository<User>();
            var user = userRepo.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

            if (user.IsUserAdminOrTrainer())
            {
                var repo = RepoResolver.GetRepository<DogSkill>();

                if (dogSkill.DogSkilID == 0)
                {
                    repo.Insert(dogSkill);
                }
                else
                {
                    repo.Update(dogSkill);
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
            var repo = RepoResolver.GetRepository<DogSkill>();
            repo.Delete(id);
            return RedirectToAction("ReadDog", new { id = repo.GetById(id).DogProfileID });
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