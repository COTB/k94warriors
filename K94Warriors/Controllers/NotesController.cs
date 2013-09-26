using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class NotesController : Controller
    {
        //
        // GET: /Notes/

        public ActionResult Index(int dogProfileId)
        {
            return View();
        }


        //
        // GET: /Notes/Edit/{dogNoteId}
        
        public ActionResult Edit(int dogNoteId)
        {
            return View();
        }


        //
        // POST: /Notes/Edit/{dogNote}
        [HttpPost]
        public ActionResult Edit(DogNote dogNote)
        {
            return View();
        }


        // 
        // GET: /Notes/Create/{dogProfileId}

        public ActionResult Create(int dogProfileId)
        {
            return View(new DogNote {DogProfileID = dogProfileId});
        }


        // 
        // POST: /Notes/Create/{dogNote}
        [HttpPost]
        public ActionResult Create(DogNote dogNote)
        {
            return View();
        }


        // 
        // POST: /Notes/Delete/{dogNoteId}
        [HttpPost]
        public ActionResult Delete(int dogNoteId)
        {
            return View();
        }
    }
}
