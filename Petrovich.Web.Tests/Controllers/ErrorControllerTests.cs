using Moq;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Petrovich.Web.Tests.Controllers
{
    public class ErrorControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly ErrorController controller;

        public ErrorControllerTests()
        {
            controller = new ErrorController(loggingServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsNotNull()
        {
            var resultGet = await controller.Index() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task BadRequest_ReturnsNotNull()
        {
            var resultGet = await controller.BadRequest() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task NotFound_ReturnsNotNull()
        {
            var resultGet = await controller.NotFound() as ViewResult;
            Assert.NotNull(resultGet);
        }
    }
}
