using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class EventType
    {
        public EventType()
        {
            DogEvents = new List<DogEvent>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public virtual ICollection<DogEvent> DogEvents { get; set; }
    }
}