using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FestiApp.Util.Util;
using FestiMS.Manager;
using FestiMS.Models;

namespace FestiMS.Util
{
    public class DiFactory
    {
        private DiFactory()
        {

        }
        public static AutofacWebApiDependencyResolver Create(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();

            builder.RegisterType<GeodanHelperService>().SingleInstance();
            builder.RegisterType<MobileServiceContext>().InstancePerLifetimeScope();
            builder.RegisterType<AuthManager>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().InstancePerLifetimeScope();
            builder.RegisterType<TokenManager>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().SingleInstance();
            var container = builder.Build();
            return new AutofacWebApiDependencyResolver(container);
        }
    }
}