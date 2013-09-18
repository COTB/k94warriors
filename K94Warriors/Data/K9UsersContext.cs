using System.Data.Entity;
using K94Warriors.Models.Accounts;

namespace K94Warriors.Data
{
    public class K9UsersContext : DbContext
    {
        public K9UsersContext()
            : base("K9")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
