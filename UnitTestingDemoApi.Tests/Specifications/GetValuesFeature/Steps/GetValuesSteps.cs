using ApprovalTests.Reporters;
using TechTalk.SpecFlow;
using TestHelpers.ApprovalTests;
using TestHelpers.WebApi;

namespace UnitTestingDemoApi.Tests.Specifications.GetValuesFeature.Steps
{
    [Binding]
    public sealed class GetValuesSteps : UnitTestingDemoApiStepsBase
    {
        [Given(@"there are two values")]
        public void GivenThereAreTwoValues()
        {
            
        }

        [When(@"I get the values")]
        public void WhenIGetTheValues()
        {
            var response = TestHelper.GetAsync("api/values").GetAwaiter().GetResult();
            Set(response);

        }

        [UseReporter(typeof(DiffReporter))]
        [Then(@"I should get an array with the two values")]
        public void ThenIShouldGetAnArrayWithTheTwoValues()
        {
            var response = Get<AssertableHttpResponse>();
            ApprovalsHelper.Verify(response.BodyAsJsonFormattedString, "ResponseWithTwoValues");
        }

    }
}
