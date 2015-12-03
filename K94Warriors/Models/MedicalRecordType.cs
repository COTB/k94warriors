using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class MedicalRecordType
    {
        [Key]
        public int MedicalRecordTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<DogMedicalRecord> DogMedicalRecords { get; set; }
    }
}