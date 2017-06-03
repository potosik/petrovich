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
    public class CategoryRepositoryTests : RepositoryTestsBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.Categories);

            categoryRepository = new CategoryRepository(contextMock.Object);
        }

        [Fact]
        public async Task FindAsync_WhenEntityFound_ReturnsCorrectEntity()
        {
            var id = new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f");
            var result = await categoryRepository.FindAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.CategoryId);
            Assert.Equal("3", result.Title);
            Assert.Equal(3, result.InventoryPart);
        }

        [Fact]
        public async Task FindAsync_WhenEntityNotFound_ReturnsNull()
        {
            var result = await categoryRepository.FindAsync(Guid.NewGuid());
            Assert.Null(result);
        }
        
        [Fact]
        public async Task ListAllAsync_WhenItemsFound_ReturnsAllItemsOrderedByCreateDescending()
        {
            var result = await categoryRepository.ListAllAsync();

            Assert.Equal(5, result.Count);
            Assert.Equal(5, result[0].InventoryPart);
            Assert.Equal(2, result[1].InventoryPart);
            Assert.Equal(3, result[2].InventoryPart);
            Assert.Equal(1, result[3].InventoryPart);
            Assert.Equal(4, result[4].InventoryPart);
        }

        [Fact]
        public async Task FindByInventoryPartAsync_WhenItemFound_ReturnsCorrectEntity()
        {
            var result = await categoryRepository.FindByInventoryPartAsync(3, new Guid("4fe3b265-5743-49f8-9b78-c24ac30e70e3"));

            Assert.NotNull(result);
            Assert.Equal(new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f"), result.CategoryId);
            Assert.Equal("3", result.Title);
            Assert.Equal(3, result.InventoryPart);
        }

        [Fact]
        public async Task FindByInventoryPartAsync_WhenEntityNotFound_ReturnsNull()
        {
            var result = await categoryRepository.FindByInventoryPartAsync(0, Guid.Empty);
            Assert.Null(result);
        }

        [Fact]
        public async Task ListUsedInventoryPartsAsync_WhenEntitiesFound_ReturnsList()
        {
            var result = await categoryRepository.ListUsedInventoryPartsAsync(new Guid("4fe3b265-5743-49f8-9b78-c24ac30e70e3"));

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(3, result[0]);
        }

        [Fact]
        public async Task ListUsedInventoryPartsAsync_WhenEntitiesNotFound_ReturnsEmptyList()
        {
            var contextMock = CreateContext().MockSet(new List<Category>().AsQueryable(), c => c.Categories);
            var categoryRepository = new CategoryRepository(contextMock.Object);
            var result = await categoryRepository.ListUsedInventoryPartsAsync(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task IsExistsForBranchAsync_WhenEntitiesFound_ReturnsTrue()
        {
            var result = await categoryRepository.IsExistsForBranchAsync(new Guid("4fe3b265-5743-49f8-9b78-c24ac30e70e3"));
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistsForBranchAsync_WhenEntitiesNotFound_ReturnsFalse()
        {
            var result = await categoryRepository.IsExistsForBranchAsync(Guid.NewGuid());
            Assert.False(result);
        }

        private static IEnumerable<Category> GetStubbedData()
        {
            return new List<Category>()
            {
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    Title = "1",
                    InventoryPart = 1,
                    BranchId = Guid.NewGuid(),
                    Created = new DateTime(100),
                },
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    Title = "2",
                    InventoryPart = 2,
                    BranchId = Guid.NewGuid(),
                    Created = new DateTime(200),
                },
                new Category()
                {
                    CategoryId = new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f"),
                    Title = "3",
                    InventoryPart = 3,
                    BranchId = new Guid("4fe3b265-5743-49f8-9b78-c24ac30e70e3"),
                    Created = new DateTime(150),
                },
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    Title = "4",
                    InventoryPart = 4,
                    BranchId = Guid.NewGuid(),
                    Created = new DateTime(50),
                },
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    Title = "5",
                    InventoryPart = 5,
                    BranchId = Guid.NewGuid(),
                    Created = new DateTime(300),
                },
            };
        }
    }
}
