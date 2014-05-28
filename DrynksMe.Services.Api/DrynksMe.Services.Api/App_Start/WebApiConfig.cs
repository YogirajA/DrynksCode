using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DrynksMe.Services.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
              name: "drink",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { controller = "Drynks", action = "Index", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            // for oData
           // config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
