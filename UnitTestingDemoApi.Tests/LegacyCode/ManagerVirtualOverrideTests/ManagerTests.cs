using System.Net;
using FakeItEasy;
using UnitTestingDemoApi.LegacyCode;
using UnitTestingDemoApi.LegacyCode.ManagerVirtualOverride;
using UnitTestingDemoApi.Tests;
using Xunit;

namespace LegacyCode.ManagerVirtualOverrideTests
{
    public class Transmit
    {
        [Fact]
        public void GivenThatAStatusCodeThatIndicatesErrorIsReturned_ThenAnErrorIsLogged()
        {
            var uploadResult = ObjectBuilder.CreateFtpUploadResult(statusCode: FtpStatusCode.ArgumentSyntaxError);
            var logger = A.Fake<ILogger>();
            var sut = new TestableManager(logger, uploadResult);

            sut.Transmit(new byte[0]);

            A.CallTo(() => logger.LogError(A<string>.Ignored)).MustHaveHappened();
        }
    }

    public class TestableManager : Manager
    {
        private readonly UploadResult _uploadResult;

        public TestableManager(ILogger logger, UploadResult uploadResult) : base(logger)
        {
            _uploadResult = uploadResult;
        }

        protected override UploadResult UploadData(byte[] bytes)
        {
            return _uploadResult;
        }
    }
}