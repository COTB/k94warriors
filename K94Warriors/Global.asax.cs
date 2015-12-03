using System.Data.Entity;
using K94Warriors.Data;
using K94Warriors.Models;
using K94Warriors.Models.Binders;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace K94Warriors
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<K9DbContext>(null);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(DogProfile), new DogProfileModelBinder());
        }

        private static void RegisterFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new EmailErrorAttribute());
            filters.Add(new HandleErrorAttribute
            {
                View = "Error"
            });

            FilterConfig.RegisterGlobalFilters(filters);
        }
    }
}