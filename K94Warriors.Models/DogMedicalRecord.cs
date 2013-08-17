using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class DogMedicalRecord
    {
        [Key]
        public int RecordID { get; set; }

        public int DogProfileID { get; set; }
        public virtual DogProfile DogProfile { get; set; }

        public string RecordType { get; set; }

        public string Title { get; set; }

        public byte[] RecordBytes { get; set; }

        public string RecordText { get; set; }

        public DateTime RecordExpiration { get; set; }

        public virtual ICollection<MetaData> MetaData { get; set; }
    }
}
