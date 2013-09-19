using System;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogImage
    {
        [Key]
        public int DogImageID { get; set; }

        public int DogProfileID { get; set; }

        public Guid BlobKey { get; set; }

        public virtual DogProfile DogProfile { get; set; }
    }
}