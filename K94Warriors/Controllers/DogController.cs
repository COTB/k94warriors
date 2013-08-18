using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class DogController : Controller
    {

        public ActionResult GetDogs()
        {
            var repo = RepoResolver.GetRepository<DogProfile>("K9");
            var dogs = repo.GetAll();
            return View(dogs);
        }

        public ActionResult CreateOrUpdateDog(DogProfile dogProfile)
        {
            throw new NotImplementedException();
        }

        public ActionResult ReadDog(int id)
        {
            throw new NotImplementedException();
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
            return RedirectToAction("GetDogs");
        }

        public ActionResult GetDocuments(int dogId)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetDocument(int documentId)
        {
            throw new NotImplementedException();
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