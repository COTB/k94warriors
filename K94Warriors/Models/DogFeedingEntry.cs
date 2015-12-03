using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogFeedingEntry
    {
        [Key]
        public int DogFeedingEntryID { get; set; }

        [Required]
        public int DogProfileID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Amount")]
        public string AmountDescription { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }

        [Display(Name = "AM Feeding")]
        public bool AMFeeding { get; set; }

        [Display(Name = "Noon Feeding")]
        public bool NoonFeeding { get; set; }

        [Display(Name = "PM Feeding")]
        public bool PMFeeding { get; set; }
        
        public string Notes { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }
    }
}