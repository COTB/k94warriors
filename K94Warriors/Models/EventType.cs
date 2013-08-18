using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class EventType
    {
        public EventType()
        {
            this.DogEvents = new List<DogEvent>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DogEvent> DogEvents { get; set; }
    }
}
