using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class Location
    {
        public Location()
        {
            this.DogProfiles = new List<DogProfile>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DogProfile> DogProfiles { get; set; }
    }
}
