using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class DogEvent
    {
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsComplete { get; set; }
        public System.DateTime EventDate { get; set; }
        public int DogProfileID { get; set; }
        public int EventTypeId { get; set; }
        public virtual DogProfile DogProfile { get; set; }
        public virtual EventType EventType { get; set; }
    }
}
