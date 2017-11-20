using Autofac;
using Place.Core.Repositories.Factory;

namespace Place.Web.Modules
{
    public class RepositoryModule : Autofac.Module
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