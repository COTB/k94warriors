using System;
using System.Collections.Generic;
using K94Warriors.Enums;

namespace K94Warriors.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedTimeUTC { get; set; }
        public string PhoneProvider { get; set; }
        public int UserTypeID { get; set; }
        public virtual ICollection<DogNote> DogNotes { get; set; }
        public virtual UserType UserType { get; set; }

        public bool IsUserAdminOrTrainer()
        {
            return (this.UserTypeID == (int)UserTypeEnum.Administrator || this.UserTypeID == (int)UserTypeEnum.Trainer);
        }
    }
}
