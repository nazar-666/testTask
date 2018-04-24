using Autofac;
using Place.Core.Repositories.Factory;

namespace Place.Web.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(Assembly.Load("Core"))
            //       .Where(t => t.Name.EndsWith("Repository"))
            //       .AsImplementedInterfaces()
            //      .InstancePerLifetimeScope();

            builder.RegisterType(typeof(RepositoryManager)).As(typeof(IRepositoryManager)).InstancePerLifetimeScope();
        }
    }
}