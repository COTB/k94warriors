using System.Data.Entity;
using K94Warriors.Data;
using K94Warriors.Migrations;
using K94Warriors.Models;
using K94Warriors.Models.Binders;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace K94Warriors
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //K9DbContext.ForceInitialize();

            Database.SetInitializer<K9DbContext>(
                new MigrateDatabaseToLatestVersion<K9DbContext, Configuration>());
            K9DbContext.ForceInitialize();
            //Database.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

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