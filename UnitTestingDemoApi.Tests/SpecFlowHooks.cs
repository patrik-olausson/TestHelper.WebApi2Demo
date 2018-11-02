using System;
using TechTalk.SpecFlow;
using TestHelpers.SpecFlow;

namespace UnitTestingDemoApi.Tests
{
    [Binding]
    public sealed class SpecFlowHooks : SpecFlowStepDefinitionBase
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            Set(new UnitTestingDemoApiHelper(Console.WriteLine));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Get<UnitTestingDemoApiHelper>().Dispose();
        }
    }
}
