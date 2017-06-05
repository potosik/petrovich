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
    public class ProductServiceTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock;

        private readonly Mock<IProductDataSource> productDataSourceMock;
        private readonly Mock<ICategoryDataSource> categoryDataSourceMock;
        private readonly Mock<IGroupDataSource> groupDataSourceMock;

        private readonly IProductService productService;

        public ProductServiceTests()
        {
            loggingServiceMock = new Mock<ILoggingService>();

            productDataSourceMock = new Mock<IProductDataSource>();
            categoryDataSourceMock = new Mock<ICategoryDataSource>();
            groupDataSourceMock = new Mock<IGroupDataSource>();

            productService = new ProductService(productDataSourceMock.Object, categoryDataSourceMock.Object, groupDataSourceMock.Object, loggingServiceMock.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenCategoryIsNull_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return productService.CreateAsync(null);
            });
        }

        [Fact]
        public async Task CreateAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return productService.CreateAsync(new Models.Product());
            });
        }

        [Fact]
        public async Task CreateAsync_WhenGroupNotFound_ThrowsGroupNotFoundException()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());

            await Assert.ThrowsAsync<GroupNotFoundException>(() =>
            {
                return productService.CreateAsync(new Models.Product() { GroupId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task CreateAsync_WhenNoSlotsForInventoryPartAvailable_ThrowsNoCategoryCategoriesSlotsException()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());

            await Assert.ThrowsAsync<NoCategoryProductsSlotsException>(() =>
            {
                return productService.CreateAsync(new Models.Product());
            });
        }

        [Fact]
        public async Task CreateAsync_WhenProductCreated_ReturnsProduct()
        {
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());
            productDataSourceMock.Setup(dataSource => dataSource.GetNewInventoryNumberAsync(It.IsAny<Guid>()))
                .ReturnsAsync(5);
            productDataSourceMock.Setup(dataSource => dataSource.CreateAsync(It.IsAny<Models.Product>()))
                .ReturnsAsync(new Models.Product());

            var result = await productService.CreateAsync(new Models.Product());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindAsync_WhenProductIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return productService.FindAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task FindAsync_WhenProductNotFound_ThrowsProductNotFoundException()
        {
            await Assert.ThrowsAsync<ProductNotFoundException>(() =>
            {
                return productService.FindAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task FindAsync_WhenProductFound_ReturnsProduct()
        {
            productDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Product());

            var result = await productService.FindAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenProductIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return productService.UpdateAsync(new Models.Product() { CategoryId = Guid.Empty });
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenProductNotFound_ThrowsProductNotFoundException()
        {
            await Assert.ThrowsAsync<ProductNotFoundException>(() =>
            {
                return productService.UpdateAsync(new Models.Product() { ProductId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenCategoryNotFound_ThrowsCategoryNotFoundException()
        {
            productDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Product() { ProductId = Guid.NewGuid() });

            await Assert.ThrowsAsync<CategoryNotFoundException>(() =>
            {
                return productService.UpdateAsync(new Models.Product() { ProductId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenGroupNotFound_ThrowsGroupNotFoundException()
        {
            productDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Product() { ProductId = Guid.NewGuid() });
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());

            await Assert.ThrowsAsync<GroupNotFoundException>(() =>
            {
                return productService.UpdateAsync(new Models.Product() { ProductId = Guid.NewGuid(), GroupId = Guid.NewGuid() });
            });
        }
        
        [Fact]
        public async Task UpdateAsync_WhenInventoryPartIsChanged_ProductInventoryPartChangedException()
        {
            productDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Product() { ProductId = Guid.NewGuid(), InventoryPart = 1 });
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());

            await Assert.ThrowsAsync<ProductInventoryPartChangedException>(() =>
            {
                return productService.UpdateAsync(new Models.Product() { ProductId = Guid.NewGuid() });
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenProductUpdated_ReturnsProduct()
        {
            productDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Product() { ProductId = Guid.NewGuid() });
            categoryDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Models.Category());
            productDataSourceMock.Setup(dataSource => dataSource.UpdateAsync(It.IsAny<Models.Product>()))
                .ReturnsAsync(new Models.Product());

            var result = await productService.UpdateAsync(new Models.Product() {  ProductId = Guid.NewGuid() });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteAsync_WhenProductIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return productService.DeleteAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task DeleteAsync_WhenProductNotFound_ThrowsProductNotFoundException()
        {
            await Assert.ThrowsAsync<ProductNotFoundException>(() =>
            {
                return productService.DeleteAsync(Guid.NewGuid());
            });
        }
    }
}
