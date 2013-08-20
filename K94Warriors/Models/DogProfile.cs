using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class DogProfile
    {
        public DogProfile()
        {
            this.DogEvents = new List<DogEvent>();
            this.DogMedicalRecords = new List<DogMedicalRecord>();
            this.DogNotes = new List<DogNote>();
            this.DogSkills = new List<DogSkill>();
            this.CreatedTimeUTC = DateTime.UtcNow;
            this.LocationId = 1; // Hacky. Too lazy to break all foreign-key relationships to make this nullable. Location 1 is not on premise and the default until changed.
        }

        public int ProfileID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public Nullable<int> Age { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> PickedUpDate { get; set; }
        public bool IsFixed { get; set; }
        public Nullable<System.DateTime> GraduationDate { get; set; }
        public Nullable<int> WarriorID { get; set; }
        public System.DateTime CreatedTimeUTC { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsApproved { get; set; }
        public int LocationId { get; set; }
        public virtual ICollection<DogEvent> DogEvents { get; set; }
        public virtual ICollection<DogMedicalRecord> DogMedicalRecords { get; set; }
        public virtual ICollection<DogNote> DogNotes { get; set; }
        public virtual WarriorInfo WarriorInfo { get; set; }
        public virtual ICollection<DogSkill> DogSkills { get; set; }
        public virtual Location Location { get; set; }
    }
}
