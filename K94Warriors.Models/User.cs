using System;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string DisplayName { get; set; }

        public DateTime CreatedTimeUTC { get; set; }

        public string PhoneProvider { get; set; }

        public int UserTypeID { get; set; }
    }
}
