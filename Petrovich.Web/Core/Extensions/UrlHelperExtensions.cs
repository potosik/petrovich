using Petrovich.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Action(this UrlHelper helper, Endpoint endpoint)
        {
            return helper.Action(endpoint.Action, endpoint.Controller);
        }
    }
}