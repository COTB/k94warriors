using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogMedicalRecord
    {
        [Key]
        public int RecordID { get; set; }

        public int DogProfileID { get; set; }

        public int MedicalRecordTypeID { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public DateTime? RecordExpirationDate { get; set; }

        [StringLength(255)]
        public string RecordURL { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }

        public virtual MedicalRecordType MedicalRecordType { get; set; }
    }
}