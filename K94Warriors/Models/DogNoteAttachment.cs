using System;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogNoteAttachment
    {
        [Key]
        public int DogNoteAttachmentID { get; set; }

        public int DogNoteID { get; set; }

        public Guid BlobKey { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public virtual DogNote DogNote { get; set; }
    }
}