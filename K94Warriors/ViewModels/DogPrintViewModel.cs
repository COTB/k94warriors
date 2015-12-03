using K94Warriors.Models;
using System.Collections.Generic;

namespace K94Warriors.ViewModels
{
    public class DogPrintViewModel
    {
        public DogProfile DogProfile { get; set; }

        public IList<DogMedicalRecord> Vaccinations { get; set; }

        public IList<DogNote> Notes { get; set; }

        public IList<DogFeedingEntry> Feeding { get; set; }

        public IList<DogImage> Images { get; set; }
    }
}