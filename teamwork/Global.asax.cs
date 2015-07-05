using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace teamwork
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            //System.Web.Http.GlobalConfiguration.Configure(config =>
            //{
            //    config.MapHttpAttributeRoutes();

            //    config.Routes.MapHttpRoute(
            //        name: "DefaultApi",
            //        routeTemplate: "api/{controller}/{id}",
            //        defaults: new { id = RouteParameter.Optional }
            //    );
            //});
        }
    }
}
