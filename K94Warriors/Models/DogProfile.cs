using System;
using System.Collections.Generic;
using K94Warriors.Enums;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogProfile
    {
        public DogProfile()
        {
            CreatedTimeUTC = DateTime.UtcNow;
            LocationId = 1;
                // Hacky. Too lazy to break all foreign-key relationships to make this nullable. Location 1 is not on premise and the default until changed.
        }

        [Key]
        public int ProfileID { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Breed { get; set; }
        
        public int? BirthYear { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Color { get; set; }
        
        public GenderEnum Gender { get; set; }
        
        public DateTime? PickedUpDate { get; set; }
        
        public bool IsFixed { get; set; }
        
        public DateTime? GraduationDate { get; set; }
        
        public DateTime CreatedTimeUTC { get; set; }
        
        public int CreatedByUserID { get; set; }
        
        public bool IsApproved { get; set; }
        
        public int LocationId { get; set; }

        public string HealthCondition { get; set; }
        
        public virtual ICollection<DogEvent> DogEvents { get; set; }
        
        public virtual ICollection<DogMedicalRecord> DogMedicalRecords { get; set; }
        
        public virtual ICollection<DogNote> DogNotes { get; set; }
        
        public virtual ICollection<DogSkill> DogSkills { get; set; }
        
        public virtual Location Location { get; set; }
        
        public virtual ICollection<DogImage> Images { get; set; }

        public virtual ICollection<DogMedication> DogMedications { get; set; }
    }
}