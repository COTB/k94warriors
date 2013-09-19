using System;
using System.Collections.Generic;
using K94Warriors.Enums;

namespace K94Warriors.Models
{
    public class DogProfile
    {
        public DogProfile()
        {
            DogEvents = new List<DogEvent>();
            DogMedicalRecords = new List<DogMedicalRecord>();
            DogNotes = new List<DogNote>();
            DogSkills = new List<DogSkill>();
            CreatedTimeUTC = DateTime.UtcNow;
            LocationId = 1;
                // Hacky. Too lazy to break all foreign-key relationships to make this nullable. Location 1 is not on premise and the default until changed.
        }

        public int ProfileID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int? BirthYear { get; set; }
        public string Color { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime? PickedUpDate { get; set; }
        public bool IsFixed { get; set; }
        public DateTime? GraduationDate { get; set; }
        public int? WarriorID { get; set; }
        public DateTime CreatedTimeUTC { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsApproved { get; set; }
        public int LocationId { get; set; }
        public virtual ICollection<DogEvent> DogEvents { get; set; }
        public virtual ICollection<DogMedicalRecord> DogMedicalRecords { get; set; }
        public virtual ICollection<DogNote> DogNotes { get; set; }
        public virtual WarriorInfo WarriorInfo { get; set; }
        public virtual ICollection<DogSkill> DogSkills { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<DogImage> Images { get; set; }
    }
}