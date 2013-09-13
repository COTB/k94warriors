using System.Web.Mvc;
using K94Warriors.Filters;

namespace K94Warriors
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Add the page title attribute
            filters.Add(new PageTitleAttribute());

/*            // Get from and to addresses from config
            var from = ConfigurationManager.AppSettings["ErrorFromEmailAddress"];
            var to = ConfigurationManager.AppSettings["ErrorToEmailAddress"];

            // Add custom error handler that will email problems out
            filters.Add(new EmailErrorAttribute(new SmtpMailer(), from, to)
            {
                // All exception types, let view specify master
                View = "Error",
            });*/

            filters.Add(new AuthorizeAttribute());
        }
    }
}