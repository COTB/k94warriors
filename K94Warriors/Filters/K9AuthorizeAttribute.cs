using K94Warriors.Data.Contracts;
using K94Warriors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Filters
{
    public class K9AuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool success = base.AuthorizeCore(httpContext);

            if (!success)
                return false;

            var user = httpContext.Session["CurrentUser"] as User;

            if (user == null)
            {
                user = ReloadUserIntoSession(httpContext);

                if (user == null)
                    return false;
            }

            return true;
        }

        public static User ReloadUserIntoSession(HttpContextBase httpContext)
        {
            string userName = httpContext.User.Identity.Name;

            var repo = DependencyResolver.Current.GetService<IRepository<User>>();

            var user = repo.Where(i => i.Email == userName).FirstOrDefault();

            httpContext.Session["CurrentUser"] = user;

            return user;
        }
    }
}