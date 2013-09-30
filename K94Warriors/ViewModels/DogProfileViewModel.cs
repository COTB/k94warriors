using System;
using System.Diagnostics;
using K94Warriors.Models;

namespace K94Warriors.ViewModels
{
    public sealed class DogProfileViewModel : DogProfile
    {
        public int? Age
        {
            get { return DateTime.Now.Year - BirthYear; }
        }


        public DogProfileViewModel()
        {
            Debug.WriteLine("DogProfileViewModel constructed.");
        }

        public DogProfileViewModel(DogProfile dogProfile) : base(dogProfile) { }
    }
}