using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using K94Warriors.ViewModels;

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
            var model = _dogNoteRepo.Where(n => n.DogProfileID == dog.ProfileID).Include(x => x.DogNoteAttachments);

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

            await DoFileUpload(model.NoteID, files);

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

            await DoFileUpload(model.NoteID, files);

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

        public ActionResult AttachmentKeys(int dogNoteId)
        {
            var dogNote = _dogNoteRepo.GetById(dogNoteId);
            if (dogNote == null)
                return null;

            var attachments = _dogNoteAttachmentRepo.Where(x => x.DogNoteID == dogNote.NoteID);
            var keys = (from a in attachments
                        select new
                            {
                                a.DogNoteAttachmentID,
                                a.FileName
                            }).ToList();

            return Json(keys, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles(NoteAttachmentUploadViewModel model)
        {
            var dogNote = _dogNoteRepo.GetById(model.DogNoteId);
            if (dogNote == null)
                return Json(new {success = false, errorMessage = "Invalid dog note id sent with request."});

            if (model.Files.Count < 1)
                return Json(new { success = false, errorMessage = "No files in request." });

            await DoFileUpload(model.DogNoteId, model.Files);

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFile(int id)
        {
            try
            {
                var attachment = _dogNoteAttachmentRepo.GetById(id);
                if (attachment == null)
                    return null;

                await _blobRepo.DeleteImageIfExistsAsync(attachment.BlobKey.ToString());
                _dogNoteAttachmentRepo.Delete(attachment);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, errorMessage = ex.Message});
            }

            return Json(new {success = true});
        }


        public async Task<ActionResult> DownloadAllFiles(int dogNoteId)
        {
            var dogNote = _dogNoteRepo.GetById(dogNoteId);
            if (dogNote == null)
                return null;

            var attachments = _dogNoteAttachmentRepo.Where(x => x.DogNoteID == dogNote.NoteID);
            if (!attachments.Any())
                return null;

            var outputStream = new MemoryStream();
            var zipStream = new ZipOutputStream(outputStream);

            zipStream.SetLevel(6); // 0-9, 9 is highest compression

            foreach (var attachment in attachments)
            {
                var entry = new ZipEntry(attachment.FileName) { DateTime = DateTime.Now };

                zipStream.PutNextEntry(entry);

                using (var stream = await _blobRepo.GetImageAsync<MemoryStream>(attachment.BlobKey.ToString()))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    StreamUtils.Copy(stream, zipStream, new byte[4096]);
                }

                zipStream.CloseEntry();
            }

            zipStream.IsStreamOwner = false;
            zipStream.Close();

            outputStream.Seek(0, SeekOrigin.Begin);
            return File(outputStream.ToArray(), "application/octet-stream", "NoteAttachments.zip");
        }

        public async Task<ActionResult> DownloadFile(int id)
        {
            var attachment = _dogNoteAttachmentRepo.GetById(id);
            if (attachment == null)
                return null;

            var stream = await _blobRepo.GetImageAsync<MemoryStream>(attachment.BlobKey.ToString());

            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), attachment.MimeType, "NoteAttachment." + attachment.FileExtension);
        }

        private async Task DoFileUpload(int dogNoteId, IEnumerable<HttpPostedFileBase> files)
        {
            if (dogNoteId == 0)
                return;

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
                    MimeType = file.ContentType,
                    FileName = file.FileName,
                    FileExtension = Path.GetExtension(file.FileName)
                };
                dogNoteAttachments.Add(dogNoteAttachment);
                await _blobRepo.InsertOrUpdateImageAsync(blobKey.ToString(), file.InputStream);
            }
            _dogNoteAttachmentRepo.Insert(dogNoteAttachments);
        }
    }
}
