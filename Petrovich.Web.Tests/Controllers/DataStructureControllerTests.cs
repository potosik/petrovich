using Moq;
using Petrovich.Business;
using Petrovich.Business.Logging;
using Petrovich.Web.Controllers;
using Petrovich.Web.Models.DataStructure;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Petrovich.Web.Tests.Controllers
{
    public class DataStructureControllerTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock = new Mock<ILoggingService>();
        private readonly Mock<IDataStructureService> dataStructureServiceMock = new Mock<IDataStructureService>();
        private readonly DataStructureController controller;

        public DataStructureControllerTests()
        {
            controller = new DataStructureController(dataStructureServiceMock.Object, loggingServiceMock.Object);
        }

        //[Fact]
        //public async Task BranchList_ReturnsNotNull()
        //{
        //    var resultGet = await controller.BranchList() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task BranchCreate_ReturnsNotNull()
        //{
        //    var resultGet = controller.BranchCreate() as ViewResult;
        //    var resultPost = await controller.BranchCreate(new CreateBranchModel()) as ViewResult;

        //    Assert.NotNull(resultGet);
        //    Assert.NotNull(resultPost);
        //}

        //[Fact]
        //public async Task BranchEdit_ReturnsNotNull()
        //{
        //    var resultGet = await controller.BranchEdit(0) as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task BranchDelete_ReturnsNotNull()
        //{
        //    var resultGet = await controller.BranchDelete() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task CategoryList_ReturnsNotNull()
        //{
        //    var resultGet = await controller.CategoryList() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task CategoryCreate_ReturnsNotNull()
        //{
        //    var resultGet = await controller.CategoryCreate() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task CategoryEdit_ReturnsNotNull()
        //{
        //    var resultGet = await controller.CategoryEdit() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task CategoryDelete_ReturnsNotNull()
        //{
        //    var resultGet = await controller.CategoryDelete() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task GroupList_ReturnsNotNull()
        //{
        //    var resultGet = await controller.GroupList() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task GroupCreate_ReturnsNotNull()
        //{
        //    var resultGet = await controller.GroupList() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task GroupEdit_ReturnsNotNull()
        //{
        //    var resultGet = await controller.GroupList() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}

        //[Fact]
        //public async Task GroupDelete_ReturnsNotNull()
        //{
        //    var resultGet = await controller.GroupList() as ViewResult;
        //    Assert.NotNull(resultGet);
        //}
    }
}
