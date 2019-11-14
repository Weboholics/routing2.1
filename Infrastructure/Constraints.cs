using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routing_20.Infrastructure
{
    public class IsValidController : IRouteConstraint
    {
        private readonly static string[] _validControllers = new string[] { "EDITOR", "LOG", "CRAWLER", "ADMIN", "SECURITY", "USER", "HELP", "CONTENT" };
        public IsValidController()
        {
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary routeValues, RouteDirection routeDirection)
        {
            string controller = routeValues[routeKey].ToString().ToUpper();
            return (_validControllers.Contains(controller));
        }
    }
}
