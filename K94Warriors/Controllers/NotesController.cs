using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Core;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class NotesController : BaseController
    {
        private readonly IRepository<DogProfile> _dogProfileRepo;
        private readonly IRepository<DogNote> _dogNoteRepo;
        private readonly IRepository<DogNoteAttachment> _dogNoteAttachmentRepo; 
        private readonly IRepository<NoteType> _noteTypeRepo;
        private readonly IBlobRepository _blobRepo;

        public NotesController(IRepository<DogNote> dogNoteRepo,
                               IRepository<NoteType> noteTypeRepo,
                               IRepository<DogNoteAttachment> dogNoteAttachmentRepo,
                               IRepository<DogProfile> dogProfileRepo,
                               IBlobRepository blobRepo)
        {
            if (dogNoteRepo == null)
                throw new ArgumentNullException("dogNoteRepo");
            _dogNoteRepo = dogNoteRepo;

            if (noteTypeRepo == null)
                throw new ArgumentNullException("noteTypeRepo");
            _noteTypeRepo = noteTypeRepo;

            if (dogNoteAttachmentRepo == null)
                throw new ArgumentNullException("dogNoteAttachmentRepo");
            _dogNoteAttachmentRepo = dogNoteAttachmentRepo;

            if (blobRepo == null)
                throw new ArgumentNullException("blobRepo");
            _blobRepo = blobRepo;

            _dogProfileRepo = dogProfileRepo;
        }


        //
        // GET: /Notes?dog={dogProfileId}

        public ActionResult Index(DogProfile dog)
        {
            var model = _dogNoteRepo.Where(n => n.DogProfileID == dog.ProfileID);

            SetDogViewBag(dog);

            return View(model);
        }


        //
        // GET: /Notes/Edit/{id}

        public ActionResult Edit(int id)
        {
            var model = _dogNoteRepo.GetById(id);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = model.DogProfile;

            SetDogViewBag(dog);

            ViewBag.NoteTypeSelectList = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", model.NoteTypeId);

            return View(model);
        }


        //
        // POST: /Notes/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(DogNote model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);

                SetDogViewBag(dog);

                ViewBag.NoteTypeSelectList = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", model.NoteTypeId);
                
                return View(model);
            }

            _dogNoteRepo.Update(model);

            await UploadFiles(model.NoteID, files);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }


        // 
        // GET: /Notes/Create?dog={dogProfileId}
        [HttpGet]
        public ActionResult Create(DogProfile dog)
        {
            var model = new DogNote { DogProfileID = dog.ProfileID };

            SetDogViewBag(dog);

            ViewBag.NoteTypeSelectList = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", model.NoteTypeId);

            return View(model);
        }


        // 
        // POST: /Notes/Create/{dogNote}
        [HttpPost]
        public async Task<ActionResult> Create(DogNote model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                var dog = _dogProfileRepo.GetById(model.DogProfileID);
                
                SetDogViewBag(dog);

                ViewBag.NoteTypeSelectList = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", model.NoteTypeId);
                
                return View(model);
            }

            model.CreatedByUserId = CurrentUserId;

            _dogNoteRepo.Insert(model);

            await UploadFiles(model.NoteID, files);

            return RedirectToAction("Index", new { dog = model.DogProfileID });
        }


        // 
        // POST: /Notes/Delete/{id}
        
        public ActionResult Delete(int id, int? dogProfileId)
        {
            _dogNoteRepo.Delete(id);

            if (dogProfileId.HasValue)
                return RedirectToAction("Index", new { dog = dogProfileId.Value });
            return RedirectToAction("Index", "Dog");
        }


        private async Task UploadFiles(int dogNoteId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null)
                return;

            var dogNoteAttachments = new List<DogNoteAttachment>();
            foreach (var file in files)
            {
                var blobKey = Guid.NewGuid();
                var dogNoteAttachment = new DogNoteAttachment
                {
                    BlobKey = blobKey,
                    DogNoteID = dogNoteId,
                    MimeType = file.ContentType
                };
                dogNoteAttachments.Add(dogNoteAttachment);
                await _blobRepo.InsertOrUpdateImageAsync(blobKey.ToString(), file.InputStream);
            }
            _dogNoteAttachmentRepo.Insert(dogNoteAttachments);
        }
    }
}
