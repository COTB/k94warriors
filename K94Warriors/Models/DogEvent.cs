using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogEvent
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }
        
        public string Body { get; set; }

        [Display(Name = "Is Complete?")]
        public bool IsComplete { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        public int DogProfileID { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public int EventTypeId { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }
        
        public virtual EventType EventType { get; set; }
    }
}