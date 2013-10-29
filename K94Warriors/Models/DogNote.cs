using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogNote
    {
        public DogNote()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int NoteID { get; set; }

        public int DogProfileID { get; set; }
        
        public string Note { get; set; }
        
        public bool IsCritical { get; set; }
        
        public int NoteTypeId { get; set; }
                
        public DateTime CreatedDate { get; set; }
        
        public int CreatedByUserId { get; set; }

        [ForeignKey("DogProfileID")]
        public virtual DogProfile DogProfile { get; set; }

        public virtual NoteType NoteType { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User User { get; set; }

        public virtual ICollection<DogNoteAttachment> DogNoteAttachments { get; set; }
    }
}