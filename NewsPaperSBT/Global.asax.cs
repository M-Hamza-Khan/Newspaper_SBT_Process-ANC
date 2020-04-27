using Sentry;
using Sentry.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewsPaperSBT
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IDisposable _sentry;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            SentryDatabaseLogging.UseBreadcrumbs();

            _sentry = SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = new Dsn(ConfigurationManager.AppSettings["SentryDsn"]);

                // Get Entity Framework integration
                o.AddEntityFramework();
            });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            // Capture unhandled exceptions
            SentrySdk.CaptureException(exception);
        }
    }
}
