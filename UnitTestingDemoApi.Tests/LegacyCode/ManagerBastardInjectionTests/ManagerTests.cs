using System.Net;
using FakeItEasy;
using UnitTestingDemoApi.LegacyCode;
using UnitTestingDemoApi.LegacyCode.ManagerBastardInjection;
using UnitTestingDemoApi.Tests;
using Xunit;

namespace LegacyCode.ManagerBastardInjectionTests
{
    public class ManagerTests
    {
        [Fact]
        public void GivenThatAStatusCodeThatIndicatesErrorIsReturned_ThenAnErrorIsLogged()
        {
            var uploadResult = ObjectBuilder.CreateFtpUploadResult(statusCode: FtpStatusCode.ArgumentSyntaxError);
            var ftpService = A.Fake<IFtpService>();
            A.CallTo(() => ftpService.UploadData(A<byte[]>.Ignored)).Returns(uploadResult);
            var logger = A.Fake<ILogger>();
            var sut = new Manager(logger, ftpService);

            sut.Transmit(new byte[0]);

            A.CallTo(() => logger.LogError(A<string>.Ignored)).MustHaveHappened();
        }
    }
}