using FakeItEasy;
using UnitTestingDemoApi.LegacyCode;
using UnitTestingDemoApi.LegacyCode.ManagerWithBoth;
using Xunit;

namespace LegacyCode.ManagerWithBothTests
{
    public class Transmit
    {
        [Fact]
        public void GivenThatAStatusCodeThatIndicatesErrorIsReturned_ThenAnErrorIsLogged()
        {
            var logger = A.Fake<ILogger>();
            var sut = new TestableManager(logger, A.Fake<IFtpService>(), indicatesError: true);

            sut.Transmit(new byte[0]);

            A.CallTo(() => logger.LogError(A<string>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void SneakyButCommonMisstake_GivenThatAStatusCodeThatIndicatesErrorIsReturned_ThenAnErrorIsLogged()
        {
            var logger = A.Fake<ILogger>();
            var sut = new TestableManager(logger);

            sut.Transmit(new byte[0]);

            A.CallTo(() => logger.LogError(A<string>.Ignored)).MustHaveHappened();
        }
    }

    public class TestableManager : Manager
    {
        private readonly bool _indicatesError;

        public TestableManager(
            ILogger logger, 
            bool indicatesError = false) : base(logger)
        {
            _indicatesError = indicatesError;
        }

        public TestableManager(
            ILogger logger, 
            IFtpService ftpService, 
            bool indicatesError = false) : base(logger, ftpService)
        {
            _indicatesError = indicatesError;
        }

        protected override bool ResultIndicatesError(UploadResult result)
        {
            return _indicatesError;
        }
    }
}