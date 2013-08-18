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

        public ActionResult GetDogs()
        {
            var repo = RepoResolver.GetRepository<DogProfile>();
            var dogs = repo.GetAll();

            throw new NotImplementedException();
        }

        public ActionResult CreateOrUpdateDog(DogProfile dogProfile)
        {
            throw new NotImplementedException();
        }

        public ActionResult ReadDog(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteDog(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetDocuments(int id)
        {
            var repo = RepoResolver.GetRepository<DogMedicalRecord>();
            var documents = repo.GetAll().Where(d => d.ProfileID == id);
            
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