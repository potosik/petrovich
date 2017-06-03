using Moq;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Petrovich.Web.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly ProductsController controller;

        public ProductsControllerTests()
        {
            controller = new ProductsController(loggingServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsNotNull()
        {
            var resultGet = await controller.Index() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task Create_ReturnsNotNull()
        {
            var resultGet = await controller.Create() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task Edit_ReturnsNotNull()
        {
            var resultGet = await controller.Edit() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task Delete_ReturnsNotNull()
        {
            var resultGet = await controller.Delete() as ViewResult;
            Assert.NotNull(resultGet);
        }
    }
}
