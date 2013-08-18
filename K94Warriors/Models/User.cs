using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class User
    {
        public System.Guid UserID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DisplayName { get; set; }
        public System.DateTime CreatedTimeUTC { get; set; }
        public string PhoneProvider { get; set; }
        public int UserTypeId { get; set; }
    }
}
