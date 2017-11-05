using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace aashrayhospital.com
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool IsLive { get; set; }

        public MvcApplication()
        {
            IsLive = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsDebugLogEnable"]) && Convert.ToBoolean(ConfigurationManager.AppSettings["IsDebugLogEnable"]);
        }

        protected void Application_Start()
        {
            LogConfig.Register();
            Helper.AppStart.Config();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
