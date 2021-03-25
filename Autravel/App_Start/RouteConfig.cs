using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Autravel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Combo",
                url: "Combo-du-lich/{text}_{id}",
                defaults: new { controller = "Products", action = "Tour", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Hotels",
                url: "Dat-phong/{text}_{id}",
                defaults: new { controller = "Products", action = "Hotel", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Post",
                url: "{controller}/{text}_{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Login", action = "index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
