using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class NoteType
    {
        public NoteType()
        {
            this.DogNotes = new List<DogNote>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DogNote> DogNotes { get; set; }
    }
}
