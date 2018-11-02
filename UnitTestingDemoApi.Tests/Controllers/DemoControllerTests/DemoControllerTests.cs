using System;
using System.Web.Http.Results;
using FakeItEasy;
using UnitTestingDemoApi.Controllers;
using UnitTestingDemoApi.Models;
using Xunit;

namespace DemoControllerTests
{
    public class GetById
    {
        [Fact]
        public void GivenThatThereDoesNotExistAnEntityThatMatchesTheId_ThenTheResponseStatusCodeShouldBeBadRequest()
        {
            var service = A.Fake<IDemoService>();
            A.CallTo(() => service.GetById(A<Guid>._)).Returns(null);
            var sut = CreateSut(service);

            var result = sut.GetById(Guid.Empty);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GivenThatTheDemoServiceThrowsAnException_ThenTheExceptionIsNotHandled()
        {
            var service = A.Fake<IDemoService>();
            A.CallTo(() => service.GetById(A<Guid>._)).Throws(new Exception("Faked error"));
            var sut = CreateSut(service);

            var exception = Assert.Throws<Exception>(() => sut.GetById(Guid.Empty));

            Assert.Equal("Faked error", exception.Message);
        }

        private DemoController CreateSut(IDemoService demoService = null)
        {
            return new DemoController(demoService ?? A.Fake<IDemoService>());
        }
    }
}