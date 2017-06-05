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
            branchDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.Branch>()))
                .ReturnsAsync(new Models.Branch());

            var result = await dataStructureService.CreateBranchAsync(new Models.Branch());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindBranchAsync_WhenBranchIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.FindBranchAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task FindBranchAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.FindBranchAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task FindBranchAsync_WhenBranchFound_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());

            var result = await dataStructureService.FindBranchAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchIdIsEmpty_ThrowsArgumentOutOfRangeException()
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
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch() { BranchId = Guid.NewGuid(), InventoryPart = "AA" });

            await Assert.ThrowsAsync<BranchInventoryPartChangedException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.Branch() { BranchId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchUpdated_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch() { BranchId = Guid.NewGuid() });
            branchDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.Branch>()))
                .ReturnsAsync(new Models.Branch());

            var result = await dataStructureService.UpdateBranchAsync(new Models.Branch() { BranchId = Guid.NewGuid() });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteBranchAsync_WhenBranchIdIsEmpty_ThrowsArgumentOutOfRangeException()
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

        [Fact]
        public async Task DeleteBranchAsync_WhenChildCategoriesFound_ThrowsChildCategoriesExistsException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());
            categoryDataSourceMock.Setup(dataSource => dataSource.IsExistsForBranchAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ChildCategoriesExistsException>(() =>
            {
                return dataStructureService.DeleteBranchAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task CreateCategoryAsync_WhenCategoryIsNull_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return dataStructureService.CreateCategoryAsync(null);
            });
        }

        [Fact]
        public async Task CreateCategoryAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.CreateCategoryAsync(new Models.Category());
            });
        }
        
        [Fact]
        public async Task CreateCategoryAsync_WhenNoSlotsForInventoryPartAvailable_ThrowsNoBranchCategoriesSlotsException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());
            categoryDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.Category>()))
                .ReturnsAsync(new Models.Category());

            await Assert.ThrowsAsync<NoBranchCategoriesSlotsException>(() =>
            {
                return dataStructureService.CreateCategoryAsync(new Models.Category());
            });
        }

        [Fact]
        public async Task CreateCategoryAsync_WhenCategoryCreated_ReturnsCategory()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());
            categoryDataSourceMock.Setup(dataSource => dataSource.GetNewInventoryNumberAsync(It.IsAny<Guid>()))
                .ReturnsAsync(5);
            categoryDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.Category>()))
                .ReturnsAsync(new Models.Category());

            var result = await dataStructureService.CreateCategoryAsync(new Models.Category());

            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task FindCategoryAsync_WhenCategoryIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.FindCategoryAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task FindCategoryAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.FindCategoryAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task FindCategoryAsync_WhenCategoryFound_ReturnsCategory()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());

            var result = await dataStructureService.FindCategoryAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.Category() { CategoryId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());

            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.Category() { CategoryId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.Category() { CategoryId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenInventoryPartIsChanged_CategoryInventoryPartChangedException()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category() { CategoryId = Guid.NewGuid(), InventoryPart = 1 });
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());

            await Assert.ThrowsAsync<CategoryInventoryPartChangedException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.Category() { CategoryId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryUpdated_ReturnsCategory()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category() { CategoryId = Guid.NewGuid() });
            categoryDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.Category>()))
                .ReturnsAsync(new Models.Category());

            var result = await dataStructureService.UpdateCategoryAsync(new Models.Category() { CategoryId = Guid.NewGuid() });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenCategoryIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.DeleteCategoryAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.DeleteCategoryAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenChildGroupsFound_ThrowsChildGroupsExistsException()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());
            groupDataSourceMock.Setup(dataSource => dataSource.IsExistsForCategoryAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ChildGroupsExistsException>(() =>
            {
                return dataStructureService.DeleteCategoryAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task ListCategoriesByBranchIdAsync_WhenBranchIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.ListCategoriesByBranchIdAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task ListCategoriesByBranchIdAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.ListCategoriesByBranchIdAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task ListCategoriesByBranchIdAsync_WhenBranchFound_ReturnsCategoryCollection()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Branch());
            categoryDataSourceMock.Setup(dataSource => dataSource.ListByBranchIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryCollection());

            var result = await dataStructureService.ListCategoriesByBranchIdAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task CreateGroupAsync_WhenGroupIsNull_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return dataStructureService.CreateGroupAsync(null);
            });
        }

        [Fact]
        public async Task CreateGroupAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.CreateGroupAsync(new Models.Group());
            });
        }

        [Fact]
        public async Task CreateGroupAsync_WhenGroupCreated_ReturnsGroup()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());
            groupDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.Group>()))
                .ReturnsAsync(new Models.Group());

            var result = await dataStructureService.CreateGroupAsync(new Models.Group());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindGroupAsync_WhenGroupIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.FindGroupAsync(Guid.Empty);
            });
        }
        
        [Fact]
        public async Task FindGroupAsync_WhenGroupNotFound_ThrowsGroupNotFoundException()
        {
            await Assert.ThrowsAsync<GroupNotFoundException>(() =>
            {
                return dataStructureService.FindGroupAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task FindGroupAsync_WhenGroupFound_ReturnsGroup()
        {
            groupDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Group());

            var result = await dataStructureService.FindGroupAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.UpdateGroupAsync(new Models.Group() { GroupId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupNotFound_ThrowsGroupNotFoundException()
        {
            await Assert.ThrowsAsync<GroupNotFoundException>(() =>
            {
                return dataStructureService.UpdateGroupAsync(new Models.Group() { GroupId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            groupDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Group());

            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.UpdateGroupAsync(new Models.Group() { GroupId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupUpdated_ReturnsGroup()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());
            groupDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Group() { GroupId = Guid.NewGuid() });
            groupDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.Group>()))
                .ReturnsAsync(new Models.Group());

            var result = await dataStructureService.UpdateGroupAsync(new Models.Group() { GroupId = Guid.NewGuid() });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteGroupAsync_WhenGroupIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.DeleteGroupAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task DeleteGroupAsync_WhenGroupNotFound_ThrowsGroupNotFoundException()
        {
            await Assert.ThrowsAsync<GroupNotFoundException>(() =>
            {
                return dataStructureService.DeleteGroupAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task ListGroupsByCategoryIdAsync_WhenBranchIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.ListGroupsByCategoryIdAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task ListGroupsByCategoryIdAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.ListGroupsByCategoryIdAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task ListGroupsByCategoryIdAsync_WhenCategoryFound_ReturnsGroupCollection()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());
            groupDataSourceMock.Setup(dataSource => dataSource.ListByCategoryIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.GroupCollection());

            var result = await dataStructureService.ListGroupsByCategoryIdAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }
    }
}
