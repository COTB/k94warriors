using System.Collections.Generic;
using System.Web;

namespace K94Warriors.ViewModels
{
    public class NoteAttachmentUploadViewModel
    {
        public int DogNoteId { get; set; }

        public IList<HttpPostedFileBase> Files { get; set; }
    }
}