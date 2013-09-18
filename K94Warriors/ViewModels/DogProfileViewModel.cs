using System;
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

        public static DogProfileViewModel FromDogProfile(DogProfile dogProfile)
        {
            return new DogProfileViewModel
                {
                    ProfileID = dogProfile.ProfileID,
                    Name = dogProfile.Name,
                    Gender = dogProfile.Gender,
                    Breed = dogProfile.Breed,
                    Age = dogProfile.Age,
                    Color = dogProfile.Color,
                    PickedUpDate = dogProfile.PickedUpDate,
                    IsFixed = dogProfile.IsFixed,
                    IsApproved = dogProfile.IsApproved
                };
        }
    }
}