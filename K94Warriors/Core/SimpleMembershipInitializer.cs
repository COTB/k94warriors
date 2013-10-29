using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using K94Warriors.Data;
using WebMatrix.WebData;

namespace K94Warriors.Core
{
    internal class SimpleMembershipInitializer
    {
        public SimpleMembershipInitializer()
        {
            Database.SetInitializer<K9UsersContext>(null);

            try
            {
                using (var context = new K9UsersContext())
                {
                    if (!context.Database.Exists())
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter) context).ObjectContext.CreateDatabase();
                    }
                }
                WebSecurity.InitializeDatabaseConnection("K9", "Users", "UserID", "Email", autoCreateTables: true);


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588",
                    ex);
            }
        }
    }
}