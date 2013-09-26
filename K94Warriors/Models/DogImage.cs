using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogImage
    {
        [Key]
        public int DogImageID { get; set; }

        public int DogProfileID { get; set; }

        public Guid BlobKey { get; set; }

        public string MimeType { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }
    }
}