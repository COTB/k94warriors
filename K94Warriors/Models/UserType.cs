using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class UserType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}