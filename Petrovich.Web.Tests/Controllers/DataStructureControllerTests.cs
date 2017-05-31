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
    public class DataStructureControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly DataStructureController controller;

        public DataStructureControllerTests()
        {
            controller = new DataStructureController(loggingServiceMock.Object);
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

        [Fact]
        public async Task SubcategoryList_ReturnsNotNull()
        {
            var resultGet = await controller.SubcategoryList() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task SubcategoryCreate_ReturnsNotNull()
        {
            var resultGet = await controller.SubcategoryCreate() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task SubcategoryEdit_ReturnsNotNull()
        {
            var resultGet = await controller.SubcategoryEdit() as ViewResult;
            Assert.NotNull(resultGet);
        }

        [Fact]
        public async Task SubcategoryDelete_ReturnsNotNull()
        {
            var resultGet = await controller.SubcategoryDelete() as ViewResult;
            Assert.NotNull(resultGet);
        }
    }
}
