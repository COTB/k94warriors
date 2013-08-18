using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class WarriorInfo
    {
        [Key]
        public int WarriorID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GraduationDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public virtual ICollection<DogProfile> DogProfiles { get; set; }
    }
}
