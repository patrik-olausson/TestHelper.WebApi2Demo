using System.Net;
using System.Threading.Tasks;
using ApprovalTests.Reporters;
using TestHelpers.ApprovalTests;
using UnitTestingDemoApi.Tests;
using Xunit;
using Xunit.Abstractions;

namespace DemoControllerTests
{
    [UseReporter(typeof(DiffReporter))]
    public class Get : DemoControllerTestharness
    {
        [Fact]
        public async Task GivenThatThereDoesNotExistAnEntityThatMatchesTheProvidedId_ThenTheResponseStatusCodeShouldBeBadRequest()
        {
            using (var testHelper = new UnitTestingDemoApiHelper(TestLogger))
            {
                var response = await testHelper.GetAsync("api/demo/f3f9c0b6-622e-4aa0-9bed-1e7fc2be6db3", ensureSuccessStatusCode: false);
                
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public void GivenThatThereExistAnEntityThatMatchesTheProvidedId_ThenTheResponseShouldContainTheExpectedEntity()
        {
            using (var testHelper = new UnitTestingDemoApiHelper(TestLogger))
            {
                var entity = ObjectBuilder.CreateDemoEntity();
                testHelper.ArrangeData(entity);

                var response = testHelper.GetAsync("api/demo/f3f9c0b6-622e-4aa0-9bed-1e7fc2be6db3").GetAwaiter().GetResult();
                
                ApprovalsHelper.Verify(response.BodyAsJsonFormattedString, "EntityThatMatchesProvidedId");
            }
        }

        public Get(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }
    }

    [UseReporter(typeof(DiffReporter))]
    public class Post : DemoControllerTestharness
    {
        [Fact]
        public async Task GivenAnEntityThatHasAllRequiredValues_ThenItReturnsStatusCodeOk()
        {
            using (var testHelper = new UnitTestingDemoApiHelper(TestLogger))
            {
                var entity = ObjectBuilder.CreateDemoEntity();

                await testHelper.PostAsJsonAsync("api/demo", entity);
            }
        }

        [Fact]
        public void GivenAnEntityThatIsMissingRequiredValue_ThenItReturnsStatusCodeBadRequestWithErrorMessage()
        {
            using (var testHelper = new UnitTestingDemoApiHelper(TestLogger))
            {
                var entity = ObjectBuilder.CreateDemoEntity(
                    importantProperty: null);

                var response = testHelper.PostAsJsonAsync("api/demo", entity, ensureSuccessStatusCode:false).GetAwaiter().GetResult();

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                ApprovalsHelper.Verify(response.BodyAsJsonFormattedString, "ErrorWhenRequiredValueIsMissing");
            }
        }

        public Post(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }
    }

    public class DemoControllerTestharness
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DemoControllerTestharness(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        protected void TestLogger(string message)
        {
            _testOutputHelper?.WriteLine(message);
        }
    }
}