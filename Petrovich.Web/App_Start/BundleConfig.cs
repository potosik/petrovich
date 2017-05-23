using System.Web.Optimization;

namespace Petrovich.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/js/vendor/jquery.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Content/js/vendor/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Content/js/vendor/"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include("~/Content/js/theme.js"));
            bundles.Add(new ScriptBundle("~/bundles/petrovich").Include("~/Content/js/petrovich.js"));
            
            bundles.Add(new StyleBundle("~/Content/css/common").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/theme").Include("~/Content/css/theme.css"));
            bundles.Add(new StyleBundle("~/Content/css/petrovich").Include("~/Content/css/petrovich.css"));
        }
    }
}
