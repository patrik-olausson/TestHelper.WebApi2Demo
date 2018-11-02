using TestHelpers.SpecFlow;

namespace UnitTestingDemoApi.Tests
{
    public class UnitTestingDemoApiStepsBase : SpecFlowStepDefinitionBase
    {
        public UnitTestingDemoApiHelper TestHelper => Get<UnitTestingDemoApiHelper>();
    }
}