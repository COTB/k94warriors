using System;
using System.Collections.Generic;
using K94Warriors.Enums;
using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models
{
    public class User
    {
        public User()
        {
            CreatedTimeUTC = DateTime.UtcNow;
        }

        [Key]
        public int UserID { get; set; }
        
        [Required]
        [StringLength(300)]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(30)]
        public string Phone { get; set; }
        
        [StringLength(200)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        
        public DateTime CreatedTimeUTC { get; set; }
        
        [StringLength(200)]
        public string PhoneProvider { get; set; }
        
        [Display(Name = "User Type")]
        public int UserTypeID { get; set; }
        
        public virtual ICollection<DogNote> DogNotes { get; set; }
        
        public virtual UserType UserType { get; set; }

        public bool IsUserAdminOrTrainer()
        {
            return (this.UserTypeID == (int)UserTypeEnum.Administrator || this.UserTypeID == (int)UserTypeEnum.Trainer);
        }
    }
}
