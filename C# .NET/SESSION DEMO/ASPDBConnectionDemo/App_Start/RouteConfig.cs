using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPDBConnectionDemo
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
                url: "students/{id}",
                defaults: new { controller = "Students", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Teacher",
                url: "teachers/{id}",
                defaults: new { controller = "Teachers", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Course",
                url: "courses/{id}",
                defaults: new { controller = "Courses", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Fee",
                url: "fees/{id}",
                defaults: new { controller = "Fees", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "StudentCourse",
                url: "studentcourses/{id}",
                defaults: new { controller = "StudentCourses", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
