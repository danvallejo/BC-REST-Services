using System.Web.Http;

namespace HelloWorldService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Filters.Add(new ExceptionHandlingAttribute());

            GlobalConfiguration.Configuration.Filters.Add(new IPExcludeAttribute());

            GlobalConfiguration.Configuration.Filters.Add(new LoggingAttribute());
        }
    }
}
