using K94Warriors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K94Warriors.ViewModels.Medication
{
    public class MedicationPrintLogViewModel
    {
        public DogProfile DogProfile { get; set; }

        public IList<DogMedication> Medications { get; set; }
    }
}