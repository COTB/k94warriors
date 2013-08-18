using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DogProfile> DogProfiles { get; set; }
    }
}
