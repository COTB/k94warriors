using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogMedication
    {
        [Key]
        public int DogMedicationID { get; set; }

        [Required]
        public int DogProfileID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Medication")]
        public string MedicationName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Amount")]
        public string AmountDescription { get; set; }

        [Display(Name = "Vet Notes")]
        public string VetNotes { get; set; }

        [Display(Name = "AM Dose")]
        public bool AMDose { get; set; }

        [Display(Name = "Noon Dose")]
        public bool NoonDose { get; set; }

        [Display(Name = "PM Dose")]
        public bool PMDose { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }
    }
}