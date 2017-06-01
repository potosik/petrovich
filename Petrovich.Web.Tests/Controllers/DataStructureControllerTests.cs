using Moq;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Petrovich.Web.Tests.Controllers
{
    public class DataStructureControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly DataStructureController controller;

        public DataStructureControllerTests()
        {
            controller = new DataStructureController(loggingServiceMock.Object);
        }

        [Fact]
        public async Task BranchList_ReturnsNotNull()
        {
            var resultGet = await controller.BranchList() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task BranchCreate_ReturnsNotNull()
        {
            var resultGet = await controller.BranchCreate() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task BranchEdit_ReturnsNotNull()
        {
            var resultGet = await controller.BranchEdit() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task BranchDelete_ReturnsNotNull()
        {
            var resultGet = await controller.BranchDelete() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task CategoryList_ReturnsNotNull()
        {
            var resultGet = await controller.CategoryList() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task CategoryCreate_ReturnsNotNull()
        {
            var resultGet = await controller.CategoryCreate() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task CategoryEdit_ReturnsNotNull()
        {
            var resultGet = await controller.CategoryEdit() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task CategoryDelete_ReturnsNotNull()
        {
            var resultGet = await controller.CategoryDelete() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task GroupList_ReturnsNotNull()
        {
            var resultGet = await controller.GroupList() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task GroupCreate_ReturnsNotNull()
        {
            var resultGet = await controller.GroupList() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task GroupEdit_ReturnsNotNull()
        {
            var resultGet = await controller.GroupList() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task GroupDelete_ReturnsNotNull()
        {
            var resultGet = await controller.GroupList() as ViewResult;
            Assert.NotNull(resultGet);
        }
    }
}
