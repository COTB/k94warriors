using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", pageTitle = "Welcome" }
            );

            routes.MapRoute(
                name: "Account",
                url: "account/{action}",
                defaults: new { controller = "Account", action = "Register", pageTitle = "Account" }
            );

   

            // 404 catch-all
            routes.MapRoute(
                name: "404 Catch All",
                url: "{*anything}",
                defaults: new { controller = "Error", action = "Error404", pageTitle = "Not Found" }
            );
        }
    }
}