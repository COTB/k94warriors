using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogNote
    {
        [Key]
        public int NoteID { get; set; }

        [Required]
        public int DogProfileID { get; set; }
        public virtual DogProfile DogProfile { get; set; }

        public string Note { get; set; }

        [Required]
        public bool IsCritical { get; set; }

        [Required]
        public int NoteTypeID { get; set; }
        public virtual NoteType NoteType { get; set; }
    }
}
