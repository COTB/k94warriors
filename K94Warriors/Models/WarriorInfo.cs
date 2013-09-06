using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class WarriorInfo
    {
        public WarriorInfo()
        {
            DogProfiles = new List<DogProfile>();
        }

        public int WarriorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime CreatedTimeUTC { get; set; }
        public Guid CreatedByUserID { get; set; }
        public virtual ICollection<DogProfile> DogProfiles { get; set; }
    }
}