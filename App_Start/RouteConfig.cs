using System.Web.Mvc;
using System.Web.Routing;

namespace Camping
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AfterRegistration",
                url: "Account/EndRegistration",
                defaults: new {controller = "Account", action = "EndRegistration"}
                );

            routes.MapRoute(
            name: "UserPage",
            url: "Profile/UserPage/{id}",
            defaults: new { controller = "Profile", action = "UserPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
