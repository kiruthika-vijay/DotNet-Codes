﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoASPNetFramework
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
                name: "Student",
                url: "student/{id}",
                defaults: new { controller = "Student", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Teacher",
                url: "teacher/{id}",
                defaults: new { controller = "Teacher", action = "Index", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Teachers",
                url: "teachers/{id}",
                defaults: new { controller = "Teachers", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
