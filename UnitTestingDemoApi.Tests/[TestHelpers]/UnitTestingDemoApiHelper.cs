using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using FakeItEasy;
using Owin;
using TestHelpers.WebApi;
using UnitTestingDemoApi.Models;

namespace UnitTestingDemoApi.Tests
{
    public class UnitTestingDemoApiHelper : WebApiTestHelper
    {
        private TestScenarioData _testData = new TestScenarioData();

        public UnitTestingDemoApiHelper(
            Action<string> testOutput,
            Action<HttpClient> configureHttpClientAction = null)
            : base(testOutput, configureHttpClientAction)
        {

        }

        protected override void ConfigureApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            WebApiConfig.Register(builder, config);
            ReplaceDependenciesThatRequiresExternalResources(builder);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseWebApi(config);
        }

        public void ArrangeData(DemoEntity entity = null)
        {
            _testData.DemoEntity = entity;
        }

        private void ReplaceDependenciesThatRequiresExternalResources(ContainerBuilder builder)
        {
            builder.RegisterInstance(CreateFakeDemoService());
        }

        private IDemoService CreateFakeDemoService()
        {
            var service = A.Fake<IDemoService>();
            A.CallTo(() => service.GetById(A<Guid>.Ignored)).Returns(_testData.DemoEntity);

            return service;
        }

        private class TestScenarioData
        {
            public DemoEntity DemoEntity { get; set; }
        }
    }
}