using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using K94Warriors.Logger;

namespace K94Warriors
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            var logger = new SmptLogger();
            var emailTo = ConfigurationManager.AppSettings["AdminErrorEmail"];
            var emailSubject = ConfigurationManager.AppSettings["AdminErrorSubject"];
            var emailFrom = ConfigurationManager.AppSettings["LogEmailFrom"];

            var message = new EmailMessage
            {
                Body = exception.ToString(),
                From = emailFrom,
                Subject = emailSubject,
                To = emailTo
            };

            logger.Send(message);

            Server.ClearError();
            Response.Redirect("~/Error/HttpError404");
        } 
    }
}