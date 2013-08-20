using System;
using System.Collections.Generic;

namespace K94Warriors.Models
{
    public class UserType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
