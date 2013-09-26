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

            // 404 catch-all
            routes.MapRoute(
                name: "404 Catch All",
                url: "{*anything}",
                defaults: new { controller = "Error", action = "Error404", pageTitle = "Not Found" }
                );
        }
    }
}