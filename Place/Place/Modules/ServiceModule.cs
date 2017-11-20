using Autofac;
using Place.Services.Services.Factory;

namespace Place.Web.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(Assembly.Load("Core"))
            //          .Where(t => t.Name.EndsWith("Service"))
            //          .AsImplementedInterfaces()
            //          .InstancePerLifetimeScope();

            builder.RegisterType(typeof(ServiceManager)).As(typeof(IServiceManager)).InstancePerLifetimeScope();
        }
    }
}