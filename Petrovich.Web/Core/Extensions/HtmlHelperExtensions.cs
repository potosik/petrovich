using Petrovich.Core.Navigation;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Petrovich.Web.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, Endpoint endpoint, object routeValues, object htmlAttributes)
        {
            return helper.ActionLink(linkText, endpoint.Action, endpoint.Controller, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, Endpoint endpoint)
        {
            return helper.ActionLink(linkText, endpoint.Action, endpoint.Controller);
        }

        public static MvcForm BeginForm(this HtmlHelper helper, Endpoint endpoint, object routeValues, FormMethod method, object htmlAttributes)
        {
            return helper.BeginForm(endpoint.Action, endpoint.Controller, routeValues, method, htmlAttributes);
        }

        public static MvcForm BeginForm(this HtmlHelper helper, Endpoint endpoint, FormMethod method, object htmlAttributes)
        {
            return helper.BeginForm(endpoint.Action, endpoint.Controller, method, htmlAttributes);
        }
    }
}