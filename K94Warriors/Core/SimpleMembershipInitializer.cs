using System;
using System.Data.Entity;
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
                WebSecurity.InitializeDatabaseConnection("K9", "Users", "UserID", "Email", true);
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