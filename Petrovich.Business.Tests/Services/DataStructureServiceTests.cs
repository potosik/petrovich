using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Business.Tests.Services
{
    public class DataStructureServiceTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock;

        private readonly Mock<IBranchDataSource> branchDataSourceMock;
        private readonly Mock<ICategoryDataSource> categoryDataSourceMock;
        private readonly Mock<IGroupDataSource> groupDataSourceMock;
        
        private readonly IDataStructureService dataStructureService;

        public DataStructureServiceTests()
        {
            loggingServiceMock = new Mock<ILoggingService>();

            branchDataSourceMock = new Mock<IBranchDataSource>();
            categoryDataSourceMock = new Mock<ICategoryDataSource>();
            groupDataSourceMock = new Mock<IGroupDataSource>();

            dataStructureService = new DataStructureService(branchDataSourceMock.Object, categoryDataSourceMock.Object, groupDataSourceMock.Object, loggingServiceMock.Object);
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchIsNull_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return dataStructureService.CreateBranchAsync(null);
            });
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchWithSameInventoryPartExists_ThrowsDuplicateBranchInventoryPartException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindByInventoryPartAsync(It.IsAny<string>()))
                .ReturnsAsync(new Models.Branch());

            await Assert.ThrowsAsync<DuplicateBranchInventoryPartException>(() =>
            {
                return dataStructureService.CreateBranchAsync(new Models.Branch());
            });
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchCreated_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.CreateBranchAsync(It.IsAny<Models.Branch>()))
                .ReturnsAsync(new Models.Branch());

            var result = await dataStructureService.CreateBranchAsync(new Models.Branch());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchIdIsZero_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.FindBranchAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.FindBranchAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchFound_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());

            var result = await dataStructureService.FindBranchAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchIdIsZero_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.Branch() { BranchId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.Branch() { BranchId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenInventoryPartIsChanged_BranchInventoryPartChangedException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch() { BranchId = Guid.NewGuid(), InventoryPart = "AA" });

            await Assert.ThrowsAsync<BranchInventoryPartChangedException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.Branch() { BranchId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchUpdated_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch() { BranchId = Guid.NewGuid() });
            branchDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.Branch>()))
                .ReturnsAsync(new Models.Branch());

            var result = await dataStructureService.UpdateBranchAsync(new Models.Branch() { BranchId = Guid.NewGuid() });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteBranchAsync_WhenBranchIdIsZero_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.DeleteBranchAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task DeleteBranchAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.DeleteBranchAsync(Guid.NewGuid());
            });
        }
    }
}
