using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using DrynksMe.Services.Api.Controllers;

namespace DrynksMe.Services.Api
{
    /// <summary>
    /// Selects which controller to serve up based on HTTP header value
    /// </summary>
    public class HeaderVersionControllerSelector : IHttpControllerSelector
    {
        //store config that gets passed on on startup
        private readonly HttpConfiguration _config;
        //dictionary to hold the list of possible controllers
        private readonly Dictionary<string, HttpControllerDescriptor> _controllers = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public HeaderVersionControllerSelector(HttpConfiguration config)
        {
            //set member variable
            _config = config;

            //manually inflate controller dictionary
            var d1 = new HttpControllerDescriptor(_config, "DrinksControllerV1", typeof(DrinksController));
            var d2 = new HttpControllerDescriptor(_config, "DrinksControllerV2", typeof(DrinksControllerV2));
            var d3 = new HttpControllerDescriptor(_config, "UserControllerV1", typeof(UserControllerV1));
            var d4 = new HttpControllerDescriptor(_config, "VenuesControllerV1", typeof(VenuesControllerV1));

            _controllers.Add("DrinksControllerV1", d1);
            _controllers.Add("DrinksControllerV2", d2);
            _controllers.Add("UserControllerV1", d3);
            _controllers.Add("VenuesControllerV1", d4);
            
        }

        /// <summary>
        /// Implement required operation and return list of controllers
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers;
        }

        /// <summary>
        /// Implement required operation that returns controller based on version, URL path
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //yank out version value from HTTP header
            IEnumerable<string> values;
            int? apiVersion = null;
            if (request.Headers.TryGetValues("X-Api-Version", out values))
            {
                foreach (string value in values)
                {
                    int version;
                    if (Int32.TryParse(value, out version))
                    {
                        apiVersion = version;
                        break;
                    }
                }
            }

            //get the name of the route used to identify the controller
            var controllerRouteName = GetControllerNameFromRequest(request);

            //build up controller name from route and version #
            var controllerName = controllerRouteName + "ControllerV" + apiVersion;

            //yank controller type out of dictionary
            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }

        /// <summary>
        /// Helper method that pulls the name of the controller from the route
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetControllerNameFromRequest(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();

            // Look up controller in route data
            object controllerName;
            routeData.Values.TryGetValue("controller", out controllerName);

            if (controllerName == null)
                return null;

            return controllerName.ToString();
        }
    }
}