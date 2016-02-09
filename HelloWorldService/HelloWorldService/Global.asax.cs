using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
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

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();

            //builder.RegisterType<ContactRepository>().As<IContactRepository>();

            var container = builder.Build();

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = 
                        new AutofacWebApiDependencyResolver(container);

        }
    }
}
