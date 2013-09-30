using System;
using K94Warriors.Models;

namespace K94Warriors.ViewModels
{
    public class DogProfileViewModel : DogProfile
    {
        public int? Age
        {
            get { return DateTime.Now.Year - Age; }
        }


        public DogProfileViewModel(DogProfile dogProfile)
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
            Location = Location;
            Images = Images;
            DogMedications = DogMedications;
        }
    }
}