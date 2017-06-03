using Moq;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Petrovich.Web.Tests.Controllers
{
    public class LoggingControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly LoggingController controller;

        public LoggingControllerTests()
        {
            controller = new LoggingController(loggingServiceMock.Object);
        }

        //[Fact]
        //public async Task Index_ReturnsNotNull()
        //{
        //    var resultGet = await controller.Index() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}
    }
}
