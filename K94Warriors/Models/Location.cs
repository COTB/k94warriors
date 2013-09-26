using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class Location
    {
        public Location()
        {
            DogProfiles = new List<DogProfile>();
        }

        [Key]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public virtual ICollection<DogProfile> DogProfiles { get; set; }
    }
}