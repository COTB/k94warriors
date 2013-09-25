using System;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogMedicalRecordImage
    {
        [Key]
        public int DogImageID { get; set; }

        public int DogMedicalRecordID { get; set; }

        public Guid BlobKey { get; set; }

        public string MimeType { get; set; }

        public virtual DogMedicalRecord DogMedicalRecord { get; set; }
    }
}