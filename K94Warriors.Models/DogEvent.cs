using System;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogEvent
    {
        [Key]
        public int EventID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
    }
}
