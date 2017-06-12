using Petrovich.Context.Entities;
using Petrovich.Repositories.Concrete;
using Petrovich.Repositories.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests
{
    public class ProductRepositoryTests : RepositoryTestsBase
    {
        private readonly IProductRepository productRepository;

        public ProductRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.Products);

            productRepository = new ProductRepository(contextMock.Object);
        }

        [Fact]
        public async Task FindAsync_WhenEntityFound_ReturnsCorrectEntity()
        {
            var id = new Guid("4ec51616-aa34-49db-949f-ef45edc9d5fb");
            var result = await productRepository.FindAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.ProductId);
            Assert.Equal("2", result.Title);
            Assert.Equal(2, result.InventoryPart);
        }

        [Fact]
        public async Task FindAsync_WhenEntityNotFound_ReturnsNull()
        {
            var result = await productRepository.FindAsync(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact]
        public async Task IsExistsForCategoryAsync_WhenEntitiesFound_ReturnsTrue()
        {
            var result = await productRepository.IsExistsForCategoryAsync(new Guid("8797509f-98fa-4155-a0d8-cd65e0afdf7a"));
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistsForCategoryAsync_WhenEntitiesNotFound_ReturnsFalse()
        {
            var result = await productRepository.IsExistsForCategoryAsync(Guid.NewGuid());
            Assert.False(result);
        }

        [Fact]
        public async Task IsExistsForGroupAsync_WhenEntitiesFound_ReturnsTrue()
        {
            var result = await productRepository.IsExistsForGroupAsync(new Guid("34440db9-3c4b-4e6d-b162-d2fe2ea862e7"));
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistsForGroupAsync_WhenEntitiesNotFound_ReturnsFalse()
        {
            var result = await productRepository.IsExistsForGroupAsync(Guid.NewGuid());
            Assert.False(result);
        }

        [Fact]
        public async Task ListUsedInventoryPartsAsync_WhenEntitiesFound_ReturnsList()
        {
            var result = await productRepository.ListUsedInventoryPartsAsync(new Guid("1b795ef2-060b-4c63-9674-23c48cebc940"));

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(3, result[0]);
        }

        [Fact]
        public async Task ListUsedInventoryPartsAsync_WhenEntitiesNotFound_ReturnsEmptyList()
        {
            var contextMock = CreateContext().MockSet(new List<Product>().AsQueryable(), c => c.Products);
            var productRepository = new ProductRepository(contextMock.Object);
            var result = await productRepository.ListUsedInventoryPartsAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task SearchFastAsync_WhenEntitiesNotFound_ReturnsEmptyList()
        {
            var result = await productRepository.SearchFastAsync("ImpossibleToFind", 10);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task SearchFastAsync_WhenEntitiesFound_RetursList()
        {
            var result = await productRepository.SearchFastAsync("1", 10);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task SearchFastCountAsync_WhenEntitiesNotFound_ReturnsZero()
        {
            var result = await productRepository.SearchFastCountAsync("ImpossibleToFind");
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task SearchFastCountAsync_WhenEntitiesFound_RetursCount()
        {
            var result = await productRepository.SearchFastCountAsync("1");
            Assert.Equal(1, result);
        }

        private static IEnumerable<Product> GetStubbedData()
        {
            return new List<Product>()
            {
                new Product()
                {
                    ProductId = Guid.NewGuid(),
                    InventoryPart = 1,
                    Title = "1",
                    CategoryId = new Guid("fb24ca7d-7ad1-4e39-83a1-f5ed2d72431d"),
                    GroupId = null,
                },
                new Product()
                {
                    ProductId = new Guid("4ec51616-aa34-49db-949f-ef45edc9d5fb"),
                    InventoryPart = 2,
                    Title = "2",
                    CategoryId = new Guid("8797509f-98fa-4155-a0d8-cd65e0afdf7a"),
                    GroupId = null,
                },
                new Product()
                {
                    ProductId = Guid.NewGuid(),
                    InventoryPart = 1,
                    Title = "3",
                    CategoryId = new Guid("1a956dc7-3c58-4f9f-9bd0-86c9bd205fa4"),
                    GroupId = new Guid("34440db9-3c4b-4e6d-b162-d2fe2ea862e7"),
                },
                new Product()
                {
                    ProductId = Guid.NewGuid(),
                    InventoryPart = 3,
                    Title = "4",
                    CategoryId = new Guid("1b795ef2-060b-4c63-9674-23c48cebc940"),
                    GroupId = new Guid("09501624-e934-46bc-a9f6-76f83420aa90"),
                },
                new Product()
                {
                    ProductId = Guid.NewGuid(),
                    InventoryPart = 2,
                    Title = "5",
                    CategoryId = new Guid("f7fb9420-d49d-44e6-88f7-bc0b60fd5ac8"),
                    GroupId = null,
                },
            };
        }
    }
}
