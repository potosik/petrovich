using Petrovich.Core.Navigation;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Action(this UrlHelper helper, Endpoint endpoint, object routeValues)
        {
            return helper.Action(endpoint.Action, endpoint.Controller, routeValues);
        }

        public static string Action(this UrlHelper helper, Endpoint endpoint)
        {
            return helper.Action(endpoint.Action, endpoint.Controller);
        }
    }
}