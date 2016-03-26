using System.Web;
using System.Web.Optimization;

namespace Camping
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/js/jquery.min.js",
                       "~/js/modernizr.custom.js",
                       "~/js/menu_jquery.js",
                       "~/js/jquery.marquee.min.js",
                       "~/js/jquery.flexisel.js",
                       "~/js/jquery.flexslider.js",
                       "~/js/easyResponsiveTabs.js",
                       "~/js/jquery-ui.js",
                       "~/js/script.js",
                       "~/js/JFCore.js",
                       "~/js/JFForms.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/bootstrap.css",
                      "~/css/style.css",
                      "~/css/flexslider.css",
                      "~/css/JFFormStyle-1.css",
                      "~/css/jquery-ui.css",
                      "~/js/jquery-ui.css"));
        }
    }
}
