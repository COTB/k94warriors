using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogProfile
    {
        [Key]
        public int ProfileID { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        [Required]
        public int Age { get; set; }

        public string Color { get; set; }

        public DateTime PickedUpDate { get; set; }
        

        [Required]
        public bool IsFixed { get; set; }

        public DateTime GraduationDate { get; set; }

        [Required]
        public int WarriorID { get; set; }

        [ForeignKey("WarriorID")]
        public virtual WarriorInfo WarriorInfo { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        public virtual ICollection<DogMedicalRecord> DogMedicalRecords { get; set; }
        public virtual ICollection<DogNote> DogNotes { get; set; }
        public virtual ICollection<WarriorInfo> WarriorInfos { get; set; }
        public virtual ICollection<DogSkill> DogSkills { get; set; }
    }
}
