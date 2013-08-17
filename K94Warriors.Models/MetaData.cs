using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class MetaData
    {
        [Key]
        public int MetaDataID { get; set; }

        public string MetaKey { get; set; }

        public string MetaValue { get; set; }

        [Required]
        public int DonorID { get; set; }
        public virtual Donor Donor { get; set; }

        [Required]
        public int WarriorID { get; set; }
        public virtual WarriorInfo WarriorInfo { get; set; }

        [Required]
        public int DogProfileID { get; set; }
        public virtual DogProfile DogProfile { get; set; }

        [Required]
        public int DogEventID { get; set; }
        public virtual DogEvent DogEvent { get; set; }

        [Required]
        public int DogNoteID { get; set; }
        public virtual DogNote DogNote { get; set; }

        [Required]
        public int MedicalRecordID { get; set; }
        public virtual DogMedicalRecord DogMedicalRecord { get; set; }
    }
}
