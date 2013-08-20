using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class EventType
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
