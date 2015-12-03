using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogCertification
    {
        [Key]
        public int DogCertificationID { get; set; }

        [Required]
        public int DogProfileID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Received")]
        public DateTime DateReceived { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }
    }
}