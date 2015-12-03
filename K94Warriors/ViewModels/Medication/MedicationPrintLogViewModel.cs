using K94Warriors.Models;
using System;
using System.Collections.Generic;

namespace K94Warriors.ViewModels.Medication
{
    public class MedicationPrintLogViewModel
    {
        public DogProfile DogProfile { get; set; }

        public IList<DogMedication> Medications { get; set; }

        public int NumDays { get; set; }

        public DateTime StartDate { get; set; }

        public List<DateTime> Days { get; set; }
    }
}