using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class DogNotesReport
    {
        public int NoteID { get; set; }
        public int DogProfileID { get; set; }
        public string Note { get; set; }
        public bool IsCritical { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public Nullable<int> Age { get; set; }
        public string Color { get; set; }
        public Nullable<System.DateTime> PickedUpDate { get; set; }
        public Nullable<int> DonorID { get; set; }
        public bool IsFixed { get; set; }
        public Nullable<System.DateTime> GraduationDate { get; set; }
        public Nullable<int> SponsorID { get; set; }
        public Nullable<int> WarriorID { get; set; }
        public System.DateTime CreatedTimeUTC { get; set; }
        public System.Guid CreatedByUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
