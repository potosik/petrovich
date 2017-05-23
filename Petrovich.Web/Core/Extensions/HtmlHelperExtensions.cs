using Petrovich.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Petrovich.Web.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, Endpoint endpoint)
        {
            return helper.ActionLink(linkText, endpoint.Action, endpoint.Controller);
        }
    }
}