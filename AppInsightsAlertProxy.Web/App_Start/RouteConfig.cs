using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppInsightsAlertProxy.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Alert", ConfigurationManager.AppSettings["AlertProxy.Id"] + "/{action}", new { controller = "Alert", action = "Index" });
            routes.MapRoute("Default", "", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
