using System;
using System.Collections.Generic;
using K94Warriors.Enums;
using K94Warriors.Models;

namespace K94Warriors.ViewModels
{
    public class DogProfileViewModel
    {
        public int ProfileID { get; set; }
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public string Breed { get; set; }
        public int? Age { get; set; }
        public string Color { get; set; }
        public DateTime? PickedUpDate { get; set; }
        public bool IsFixed { get; set; }
        public bool IsApproved { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime DateCreated { get; set; }
        public Location Location { get; set; }
        public IEnumerable<DogNote> Notes { get; set; }
        public string HealthCondition { get; set; }

        /// <summary>
        /// Returns a DogProfile object updated with the viewmodel's data. 
        /// A new DogProfile if no parameter is provided. Updates an existing
        /// DogProfile if one is provided.
        /// </summary>
        /// <param name="profileToUpdate"></param>
        /// <returns></returns>
        public DogProfile ToDogProfile(DogProfile profileToUpdate = null)
        {
            var profile = profileToUpdate ?? new DogProfile();

            profile.Name = Name;
            profile.Gender = Gender;
            profile.Breed = Breed;
            profile.BirthYear = DateTime.Now.Year - Age;
            profile.Color = Color;
            profile.PickedUpDate = PickedUpDate;
            profile.IsFixed = IsFixed;
            profile.IsApproved = IsApproved;
            profile.HealthCondition = HealthCondition;

            return profile;
        }

        public static DogProfileViewModel FromDogProfile(DogProfile dogProfile)
        {
            return new DogProfileViewModel
                {
                    ProfileID = dogProfile.ProfileID,
                    Name = dogProfile.Name,
                    Gender = dogProfile.Gender,
                    Breed = dogProfile.Breed,
                    Age = DateTime.Now.Year - dogProfile.BirthYear,
                    Color = dogProfile.Color,
                    PickedUpDate = dogProfile.PickedUpDate,
                    IsFixed = dogProfile.IsFixed,
                    IsApproved = dogProfile.IsApproved,
                    DateCreated = dogProfile.CreatedTimeUTC,
                    Location = dogProfile.Location,
                    Notes = dogProfile.DogNotes,
                    HealthCondition = dogProfile.HealthCondition
                };
        }
    }
}