using ApprovalTests.Reporters;
using TestHelpers.ApprovalTests;
using UnitTestingDemoApi.Tests;
using Xunit;
using Xunit.Abstractions;

namespace ValuesControllerTests
{
    public class Get
    {
        [UseReporter(typeof(DiffReporter))]
        [Fact]
        public void GivenThatThereAreTwoValues_ThenItReturnsAnArrayWithTwoValues()
        {
            using (var testHelper = new UnitTestingDemoApiHelper(_outputHelper.WriteLine))
            {
                var response = testHelper.GetAsync("api/values").GetAwaiter().GetResult();

                ApprovalsHelper.Verify(response.BodyAsJsonFormattedString, "ResponseWithTwoValues");
            }
        }

        private readonly ITestOutputHelper _outputHelper;
        public Get(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
    }
}