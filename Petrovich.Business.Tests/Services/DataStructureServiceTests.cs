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
        private readonly Mock<IProductDataSource> productDataSourceMock;

        private readonly IDataStructureService dataStructureService;

        public DataStructureServiceTests()
        {
            loggingServiceMock = new Mock<ILoggingService>();

            branchDataSourceMock = new Mock<IBranchDataSource>();
            categoryDataSourceMock = new Mock<ICategoryDataSource>();
            groupDataSourceMock = new Mock<IGroupDataSource>();
            productDataSourceMock = new Mock<IProductDataSource>();

            dataStructureService = new DataStructureService(branchDataSourceMock.Object, categoryDataSourceMock.Object, 
                groupDataSourceMock.Object, productDataSourceMock.Object, loggingServiceMock.Object);
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
                .ReturnsAsync(new Models.BranchModel());

            await Assert.ThrowsAsync<DuplicateBranchInventoryPartException>(() =>
            {
                return dataStructureService.CreateBranchAsync(new Models.BranchModel());
            });
        }

        [Fact]
        public async Task CreateBranchAsync_WhenBranchCreated_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.BranchModel>()))
                .ReturnsAsync(new Models.BranchModel());

            var result = await dataStructureService.CreateBranchAsync(new Models.BranchModel());

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
                .ReturnsAsync(new Models.BranchModel());

            var result = await dataStructureService.FindBranchAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.BranchModel() { BranchId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.BranchModel() { BranchId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenInventoryPartIsChanged_BranchInventoryPartChangedException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel() { BranchId = Guid.NewGuid(), InventoryPart = "AA" });

            await Assert.ThrowsAsync<BranchInventoryPartChangedException>(() =>
            {
                return dataStructureService.UpdateBranchAsync(new Models.BranchModel() { BranchId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateBranchAsync_WhenBranchUpdated_ReturnsBranch()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel() { BranchId = Guid.NewGuid() });
            branchDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.BranchModel>()))
                .ReturnsAsync(new Models.BranchModel());

            var result = await dataStructureService.UpdateBranchAsync(new Models.BranchModel() { BranchId = Guid.NewGuid() });

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
                .ReturnsAsync(new Models.BranchModel());
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
                return dataStructureService.CreateCategoryAsync(new Models.CategoryModel());
            });
        }
        
        [Fact]
        public async Task CreateCategoryAsync_WhenNoSlotsForInventoryPartAvailable_ThrowsNoBranchCategoriesSlotsException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());
            categoryDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.CategoryModel>()))
                .ReturnsAsync(new Models.CategoryModel());

            await Assert.ThrowsAsync<NoBranchCategoriesSlotsException>(() =>
            {
                return dataStructureService.CreateCategoryAsync(new Models.CategoryModel());
            });
        }

        [Fact]
        public async Task CreateCategoryAsync_WhenCategoryCreated_ReturnsCategory()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());
            categoryDataSourceMock.Setup(dataSource => dataSource.GetNewInventoryNumberAsync(It.IsAny<Guid>()))
                .ReturnsAsync(5);
            categoryDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.CategoryModel>()))
                .ReturnsAsync(new Models.CategoryModel());

            var result = await dataStructureService.CreateCategoryAsync(new Models.CategoryModel());

            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task CreateCategoryAsync_WhenBasePriceIsNotSpecified_PriceTypeShouldBeNull()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());
            categoryDataSourceMock.Setup(dataSource => dataSource.GetNewInventoryNumberAsync(It.IsAny<Guid>()))
                .ReturnsAsync(5);
            categoryDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.CategoryModel>()))
                .ReturnsAsync(new Models.CategoryModel());

            var result = await dataStructureService.CreateCategoryAsync(new Models.CategoryModel() { PriceType = Models.Enumerations.PriceTypeBusiness.Month });

            Assert.NotNull(result);
            Assert.Null(result.Price);
            Assert.Null(result.PriceType);
        }

        [Fact]
        public async Task CreateCategoryAsync_WhenBasePriceTypeIsNotSpecified_PriceShouldBeNull()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());
            categoryDataSourceMock.Setup(dataSource => dataSource.GetNewInventoryNumberAsync(It.IsAny<Guid>()))
                .ReturnsAsync(5);
            categoryDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.CategoryModel>()))
                .ReturnsAsync(new Models.CategoryModel());

            var result = await dataStructureService.CreateCategoryAsync(new Models.CategoryModel() { Price = 1f });

            Assert.NotNull(result);
            Assert.Null(result.Price);
            Assert.Null(result.PriceType);
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
                .ReturnsAsync(new Models.CategoryModel());

            var result = await dataStructureService.FindCategoryAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.CategoryModel() { CategoryId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());

            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.CategoryModel() { CategoryId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenBranchNotFound_ThrowsBranchNotFoundException()
        {
            await Assert.ThrowsAsync<BranchNotFoundException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.CategoryModel() { CategoryId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenInventoryPartIsChanged_CategoryInventoryPartChangedException()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel() { CategoryId = Guid.NewGuid(), InventoryPart = 1 });
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());

            await Assert.ThrowsAsync<CategoryInventoryPartChangedException>(() =>
            {
                return dataStructureService.UpdateCategoryAsync(new Models.CategoryModel() { CategoryId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryUpdated_ReturnsCategory()
        {
            branchDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.BranchModel());
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel() { CategoryId = Guid.NewGuid() });
            categoryDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.CategoryModel>()))
                .ReturnsAsync(new Models.CategoryModel());

            var result = await dataStructureService.UpdateCategoryAsync(new Models.CategoryModel() { CategoryId = Guid.NewGuid() });

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
                .ReturnsAsync(new Models.CategoryModel());
            groupDataSourceMock.Setup(dataSource => dataSource.IsExistsForCategoryAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ChildGroupsExistsException>(() =>
            {
                return dataStructureService.DeleteCategoryAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task DeleteCategoryAsync_WhenChildProductsExists_ThrowsChildProductsExistsException()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel());
            productDataSourceMock.Setup(dataSource => dataSource.IsExistsForCategoryAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ChildProductsExistsException>(() =>
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
                .ReturnsAsync(new Models.BranchModel());
            categoryDataSourceMock.Setup(dataSource => dataSource.ListByBranchIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModelCollection());

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
                return dataStructureService.CreateGroupAsync(new Models.GroupModel());
            });
        }

        [Fact]
        public async Task CreateGroupAsync_WhenGroupCreated_ReturnsGroup()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel());
            groupDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.GroupModel>()))
                .ReturnsAsync(new Models.GroupModel());

            var result = await dataStructureService.CreateGroupAsync(new Models.GroupModel());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateGroupAsync_WhenPriceIsNotSpecified_PriceTypeShouldBeNull()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel());
            groupDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.GroupModel>()))
                .ReturnsAsync(new Models.GroupModel());

            var result = await dataStructureService.CreateGroupAsync(new Models.GroupModel() { PriceType = Models.Enumerations.PriceTypeBusiness.Month });

            Assert.NotNull(result);
            Assert.Null(result.Price);
            Assert.Null(result.PriceType);
        }

        [Fact]
        public async Task CreateGroupAsync_WhenBasePriceTypeIsNotSpecified_PriceShouldBeNull()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel());
            groupDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.GroupModel>()))
                .ReturnsAsync(new Models.GroupModel());

            var result = await dataStructureService.CreateGroupAsync(new Models.GroupModel() { Price = 1f });

            Assert.NotNull(result);
            Assert.Null(result.Price);
            Assert.Null(result.PriceType);
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
                .ReturnsAsync(new Models.GroupModel());

            var result = await dataStructureService.FindGroupAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return dataStructureService.UpdateGroupAsync(new Models.GroupModel() { GroupId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupNotFound_ThrowsGroupNotFoundException()
        {
            await Assert.ThrowsAsync<GroupNotFoundException>(() =>
            {
                return dataStructureService.UpdateGroupAsync(new Models.GroupModel() { GroupId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            groupDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.GroupModel());

            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return dataStructureService.UpdateGroupAsync(new Models.GroupModel() { GroupId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupUpdated_ReturnsGroup()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.CategoryModel());
            groupDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.GroupModel() { GroupId = Guid.NewGuid() });
            groupDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.GroupModel>()))
                .ReturnsAsync(new Models.GroupModel());

            var result = await dataStructureService.UpdateGroupAsync(new Models.GroupModel() { GroupId = Guid.NewGuid() });

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
        public async Task DeleteGroupAsync_WhenChildProductsExists_ThrowsChildProductsExistsException()
        {
            groupDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.GroupModel());
            productDataSourceMock.Setup(dataSource => dataSource.IsExistsForGroupAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ChildProductsExistsException>(() =>
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
                .ReturnsAsync(new Models.CategoryModel());
            groupDataSourceMock.Setup(dataSource => dataSource.ListByCategoryIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.GroupModelCollection());

            var result = await dataStructureService.ListGroupsByCategoryIdAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }
    }
}
