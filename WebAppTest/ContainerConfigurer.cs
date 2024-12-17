using Autofac;
using Autofac.Integration.Mvc;
using Repository_DBFirst;
using System.Reflection;
using System.Web.Mvc;
using BusinessLayer;

namespace WebAppTest
{
    public class ContainerConfigurer
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();


            // Înregistrăm toate controller-ele din aplicație (pentru MVC)
            builder.RegisterControllers(Assembly.GetExecutingAssembly());


            // Înregistrăm contextul bazei de date
            builder.RegisterType<photoeditEntities>()
                .As<IPhotoeditEntities>()
                   .InstancePerRequest(); // Scoped - un obiect pe request

            builder.RegisterType<MemoryCacheService>()
               .As<ICache>()
               .InstancePerLifetimeScope();

            builder.RegisterType<FotografiiService>()
                               .As<IFotografiiService>()
                               .InstancePerRequest();  // Singleton: unică instanță

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Construim containerul
            var container = builder.Build();


            // Setăm resolver-ul de dependență pentru MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
