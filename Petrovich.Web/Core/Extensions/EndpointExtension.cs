using Petrovich.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class EndpointExtension
    {
        public static string GetLink(this Endpoint endpoint, object routeValues)
        {
            return GetUrlHelper().Action(endpoint, routeValues);
        }
        
        private static UrlHelper GetUrlHelper()
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext);
        }
    }
}