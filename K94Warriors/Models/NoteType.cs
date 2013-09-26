using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class NoteType
    {
        public NoteType()
        {
            DogNotes = new List<DogNote>();
        }

        [Key]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public virtual ICollection<DogNote> DogNotes { get; set; }
    }
}