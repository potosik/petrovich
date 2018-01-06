using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Petrovich.Core.Extensions
{
    public static class UriExtensions
    {
        public static Uri SetParameter(this Uri uri, string paramName, string value)
        {
            var queryParts = HttpUtility.ParseQueryString(uri.Query);
            queryParts[paramName] = value;
            return new Uri(uri.AbsoluteUriExcludingQuery() + '?' + queryParts.ToString() + uri.Fragment);
        }

        private static string AbsoluteUriExcludingQuery(this Uri uri)
        {
            return uri.AbsoluteUri.Split('?').FirstOrDefault() ?? String.Empty;
        }
    }
}
