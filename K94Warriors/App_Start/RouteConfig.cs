using System.Web.Mvc;
using System.Web.Routing;

namespace K94Warriors
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Home",
            //    url: "",
            //    defaults: new { controller = "Home", action = "Index", pageTitle = "Welcome" }
            //    );

            //routes.MapRoute(
            //    name: "Account",
            //    url: "Account/{action}",
            //    defaults: new { controller = "Account", action = "Register", pageTitle = "Account" }
            //    );

            //routes.MapRoute(
            //    name: "Dog",
            //    url: "Dog/{action}/{id}",
            //    defaults: new { controller = "Dog", action = "Index", pageTitle = "Dogs", id = "" }
            //    );

            //routes.MapRoute(
            //    name: "Events",
            //    url: "Events/{action}/{id}",
            //    defaults: new { controller = "Events", action = "Index", pageTitle = "Events", id = "" }
            //    );

            //routes.MapRoute(
            //    name: "MedicalRecords",
            //    url: "Medicalrecords/{action}/{id}",
            //    defaults: new { controller = "MedicalRecords", action = "Index", pageTitle = "MedicalRecords", id = "" }
            //    );

            //routes.MapRoute(
            //    name: "Notes",
            //    url: "Notes/{action}/{id}",
            //    defaults: new { controller = "Notes", action = "Index", pageTitle = "Notes", id = "" }
            //    );

            // 404 catch-all
            routes.MapRoute(
                name: "404 Catch All",
                url: "{*anything}",
                defaults: new { controller = "Error", action = "Error404", pageTitle = "Not Found" }
                );
        }
    }
}