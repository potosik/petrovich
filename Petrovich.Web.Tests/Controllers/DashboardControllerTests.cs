using Moq;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Petrovich.Web.Tests.Controllers
{
    public class DashboardControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly DashboardController controller;

        public DashboardControllerTests()
        {
            controller = new DashboardController(loggingServiceMock.Object);
        }

        [Fact]
        public void Index_ReturnsNotNull()
        {
            var resultGet = controller.Index() as ViewResult;
            Assert.NotNull(resultGet);
        }
    }
}
