using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class Donor
    {
        [Key]
        public int DonorID { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [Required]
        public DateTime InspectionDate { get; set; }

        [Required]
        public DateTime ApprovedDate { get; set; }

        public virtual ICollection<DogProfile> DogProfiles { get; set; }

    }
}
