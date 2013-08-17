using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class UserType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
