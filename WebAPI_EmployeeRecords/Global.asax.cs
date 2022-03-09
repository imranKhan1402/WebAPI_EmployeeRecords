using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAPI_EmployeeRecords.Core.Model.Utility;

namespace WebAPI_EmployeeRecords
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var ServerName = ConfigurationManager.AppSettings["ServerName"];
            var DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            var UserId = ConfigurationManager.AppSettings["UserId"];
            var Password = ConfigurationManager.AppSettings["Password"];

            DatabaseConfiguration.ConnectionString = @"Data Source =" + ServerName +
                                                        "; Initial Catalog =" + DatabaseName +
                                                        "; User ID = " + UserId +
                                                        ";packet size=4096;Connection Lifetime=202130;Pooling=false;Connect Timeout=202545; Password= " + Password;
        }
    }
}
