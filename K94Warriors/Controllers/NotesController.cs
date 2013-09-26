using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;

namespace K94Warriors.Controllers
{
    public class NotesController : Controller
    {
        private readonly IRepository<DogProfile> _dogRepo;
        private readonly IRepository<DogNote> _dogNoteRepo;
        private readonly IRepository<DogNoteAttachment> _dogNoteAttachmentRepo; 
        private readonly IRepository<NoteType> _noteTypeRepo;
        private readonly IBlobRepository _blobRepo;

        public NotesController(IRepository<DogProfile> dogRepo,
                               IRepository<DogNote> dogNoteRepo,
                               IRepository<NoteType> noteTypeRepo,
                               IRepository<DogNoteAttachment> dogNoteAttachmentRepo,
                               IBlobRepository blobRepo)
        {
            if (dogRepo == null)
                throw new ArgumentNullException("dogRepo");
            _dogRepo = dogRepo;

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
        }


        //
        // GET: /Notes/

        public ActionResult Index(int dogProfileId)
        {
            var model = _dogNoteRepo.Where(n => n.DogProfileID == dogProfileId);
            var dog = _dogRepo.GetById(dogProfileId);

            ViewBag.DogId = dog.ProfileID;

            return View(model);
        }


        //
        // GET: /Notes/Edit/{dogNoteId}

        public ActionResult Edit(int dogNoteId)
        {
            var model = _dogNoteRepo.GetById(dogNoteId);
            if (model == null)
                return RedirectToAction("Index", "Dog");

            var dog = model.DogProfile;

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;
            ViewBag.NoteTypeSelectList = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", model.NoteTypeId);

            return View(model);
        }


        //
        // POST: /Notes/Edit/{dogNote}
        [HttpPost]
        public async Task<ActionResult> Edit(DogNote model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(model);

            _dogNoteRepo.Update(model);

            await UploadFiles(model.NoteID, files);

            return RedirectToAction("Index", new { dogProfileId = model.DogProfileID });
        }


        // 
        // GET: /Notes/Create/{dogProfileId}

        public ActionResult Create(int dogProfileId)
        {
            var dog = _dogRepo.GetById(dogProfileId);
            var model = new DogNote { DogProfileID = dog.ProfileID };

            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;
            ViewBag.NoteTypeSelectList = new SelectList(_noteTypeRepo.GetAll(), "ID", "Name", model.NoteTypeId);

            return View(new DogNote { DogProfileID = dogProfileId });
        }


        // 
        // POST: /Notes/Create/{dogNote}
        [HttpPost]
        public async Task<ActionResult> Create(DogNote model, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(model);

            _dogNoteRepo.Insert(model);

            await UploadFiles(model.NoteID, files);

            return RedirectToAction("Index", new { dogProfileId = model.DogProfileID });
        }


        // 
        // POST: /Notes/Delete/{dogNoteId}
        [HttpPost]
        public ActionResult Delete(int dogNoteId, int? dogProfileId)
        {
            _dogNoteRepo.Delete(dogNoteId);

            if (dogProfileId.HasValue)
                return RedirectToAction("Index", new { dogProfileId = dogProfileId.Value });
            return RedirectToAction("Index", "Dog");
        }


        private async Task UploadFiles(int dogNoteId, IEnumerable<HttpPostedFileBase> files)
        {
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
