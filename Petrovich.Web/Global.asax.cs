using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Petrovich.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();

            var date = "dd.MM.yyyy";
            var time = "HH:mm:ss";
            newCulture.DateTimeFormat.ShortDatePattern = date;
            newCulture.DateTimeFormat.ShortTimePattern = time;
            newCulture.DateTimeFormat.LongDatePattern = date;
            newCulture.DateTimeFormat.LongTimePattern = time;
            newCulture.DateTimeFormat.FullDateTimePattern = $"{date} {time}";
            
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
    }
}
