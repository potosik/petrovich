using Moq;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task Index_ReturnsNotNull()
        {
            var resultGet = await controller.Index() as ViewResult;
            Assert.NotNull(resultGet);
        }
    }
}
