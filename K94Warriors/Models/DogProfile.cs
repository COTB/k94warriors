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

        public DogProfile(DogProfile dogProfile)
        {
            InitWithDogProfile(dogProfile);
        }

        private void InitWithDogProfile(DogProfile dogProfile)
        {
            ProfileID = dogProfile.ProfileID;
            Name = dogProfile.Name;
            Breed = dogProfile.Breed;
            BirthYear = dogProfile.BirthYear;
            Color = dogProfile.Color;
            Gender = dogProfile.Gender;
            PickedUpDate = dogProfile.PickedUpDate;
            IsFixed = dogProfile.IsFixed;
            GraduationDate = dogProfile.GraduationDate;
            CreatedTimeUTC = dogProfile.CreatedTimeUTC;
            CreatedByUserID = dogProfile.CreatedByUserID;
            IsApproved = dogProfile.IsApproved;
            LocationId = dogProfile.LocationId;
            HealthCondition = dogProfile.HealthCondition;
            DogEvents = dogProfile.DogEvents;
            DogMedicalRecords = dogProfile.DogMedicalRecords;
            DogNotes = dogProfile.DogNotes;
            DogSkills = dogProfile.DogSkills;
            Location = dogProfile.Location;
            Images = dogProfile.Images;
            DogMedications = dogProfile.DogMedications;
            LocationDescription = dogProfile.LocationDescription;
        }

        [Key]
        public int ProfileID { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Breed { get; set; }
        
        [Display(Name = "Birth Year")]
        public int? BirthYear { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Color { get; set; }
        
        public GenderEnum Gender { get; set; }
        
        [Display(Name = "Picked Up Date")]
        public DateTime? PickedUpDate { get; set; }
        
        [Display(Name = "Is Fixed?")]
        public bool IsFixed { get; set; }
        
        public DateTime? GraduationDate { get; set; }
        
        public DateTime CreatedTimeUTC { get; set; }
        
        public int CreatedByUserID { get; set; }
        
        [Display(Name = "Is Approved?")]
        public bool IsApproved { get; set; }
        
        public int LocationId { get; set; }

        [Display(Name = "Health Condition")]
        public string HealthCondition { get; set; }

        [Display(Name = "Location")]
        public string LocationDescription { get; set; }

        public bool Deleted { get; set; }
        
        public virtual ICollection<DogEvent> DogEvents { get; set; }
        
        public virtual ICollection<DogMedicalRecord> DogMedicalRecords { get; set; }
        
        public virtual ICollection<DogNote> DogNotes { get; set; }
        
        public virtual ICollection<DogSkill> DogSkills { get; set; }
        
        public virtual Location Location { get; set; }
        
        public virtual ICollection<DogImage> Images { get; set; }

        public virtual ICollection<DogMedication> DogMedications { get; set; }

        public virtual ICollection<DogFeedingEntry> DogFeedingEntries { get; set; }

        public virtual ICollection<DogCertification> DogCertifications { get; set; }
    }
}