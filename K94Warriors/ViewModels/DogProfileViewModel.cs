using System;
using K94Warriors.Models;

namespace K94Warriors.ViewModels
{
    public sealed class DogProfileViewModel : DogProfile
    {
        public int? Age => DateTime.Now.Year - BirthYear;

        public DogProfileViewModel()
        {
        }

        public DogProfileViewModel(DogProfile dogProfile) 
            : base(dogProfile) 
        { 
        }
    }
}