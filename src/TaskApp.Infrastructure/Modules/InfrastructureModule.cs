using TaskApp.Infrastructure.MongoDataAccess;

namespace TaskApp.Infrastructure.Modules
{
    using Autofac;

    public class InfrastructureModule : Autofac.Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>()
                .As<Context>()
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("databaseName", DatabaseName)
                .SingleInstance();

            //
            // Register all Types in MongoDataAccess namespace
            //
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(type => type.Namespace.Contains("MongoDataAccess"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
