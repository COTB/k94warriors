using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using K94Warriors.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using System.IO;


namespace K94Warriors.Controllers
{
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

        [Authorize]
        [HttpPost]
        public ActionResult CreateOrUpdateDog(DogProfile dogProfile)
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            var userRep = RepoResolver.GetRepository<User>();
            var user = userRep.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

            if (user.UserType.Name != "ADMIN" && user.UserType.Name != "TRAINER")
                return RedirectToAction("Index");

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
            
            return Json(documents.ToList(), JsonRequestBehavior.AllowGet);
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

        [Authorize]
        [HttpPost]
        public ActionResult CreateOrUpdateDogNote(DogNote dogNote)
        {
            if (dogNote.NoteID == 0)
            {
                var repo = RepoResolver.GetRepository<DogNote>();
                var userRepo = RepoResolver.GetRepository<User>();
                var user = userRepo.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

                dogNote.CreatedDate = DateTime.UtcNow;
                dogNote.CreatedByUserId = user.UserID;

                repo.Insert(dogNote);
            }
            return RedirectToAction("ReadDog", new {id = dogNote.DogProfileID});
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateOrUpdateDogNote(int dogId, int? noteId)
        {
            DogNote viewModel;
            var dog = _dogRepo.GetById(dogId);
            var repo = RepoResolver.GetRepository<DogNote>();
            var noteTypeRepo = RepoResolver.GetRepository<NoteType>();

            viewModel = noteId.HasValue ? repo.GetById(noteId.Value) : new DogNote {DogProfileID = dogId};

            ViewBag.NoteTypeId = new SelectList(noteTypeRepo.GetAll(), "ID", "Name", viewModel.NoteTypeId);
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
            var repo = RepoResolver.GetRepository<DogNote>();
            var model = repo.Where(n => n.DogProfileID == dogId);
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