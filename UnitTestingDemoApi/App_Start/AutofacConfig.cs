using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace UnitTestingDemoApi
{
    public class AutofacConfig
    {
        public static void RegisterTypes(ContainerBuilder builder, HttpConfiguration config)
        {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}