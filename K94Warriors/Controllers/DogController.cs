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

            if (id.HasValue)
            {
                viewModel = _dogRepo.GetById(id.Value);
            }
            else
            {
                viewModel = new DogProfile();
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateOrUpdateDog(DogProfile dogProfile)
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            var userRep = RepoResolver.GetRepository<User>();
            var user = userRep.Where(u => u.Email == this.HttpContext.User.Identity.Name).FirstOrDefault();

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

        public ActionResult CreateOrUpdateDogNote(DogNote dogNote)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetNotes(int dogId)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetNotes(int dogId, int noteTypeId)
        {
            throw new NotImplementedException();
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