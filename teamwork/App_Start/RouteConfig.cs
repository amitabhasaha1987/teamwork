using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace teamwork
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );


            //routes.MapRoute(
            //    name: "ProductImageUpload",
            //    url: "{controller}/{action}/{id}/{type}/{category}",
            //    defaults: new { controller = "Products", action = "ProductImage", id = UrlParameter.Optional, type = UrlParameter.Optional, category=UrlParameter.Optional }
            //);
        }
    }
}
