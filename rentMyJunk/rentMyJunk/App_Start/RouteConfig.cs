using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rentMyJunk
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
            routes.MapRoute(
               name: "Items",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Items", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ItemsByCat",
               url: "{controller}/{action}/{cat}",
               defaults: new { controller = "Items", action = "ByCategory" }
           );
        }
    }
}
