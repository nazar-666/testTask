using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Place.Web.Modules;

namespace Place.Web
{
    public class AutoFacConfig
    {
        public static void RegisteModules()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}