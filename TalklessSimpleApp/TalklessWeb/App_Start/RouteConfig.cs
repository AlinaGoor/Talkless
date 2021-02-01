using System.Web.Mvc;
using System.Web.Routing;

namespace TalklessWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Profile",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Message",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Message", action = "Index", id = UrlParameter.Optional });
        }
    }
}
