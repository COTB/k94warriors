using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K94Warriors.Models
{
    public class DogMedicalRecord
    {
        [Key]
        public int RecordID { get; set; }

        [Column("DogProfileID")]
        public int ProfileID { get; set; }
        
        public virtual DogProfile DogProfile { get; set; }

        public string RecordType { get; set; }

        public string Title { get; set; }

        public string RecordURL { get; set; }

        public DateTime RecordExpirationDate { get; set; }

        //public virtual ICollection<MetaData> MetaData { get; set; }
    }
}
