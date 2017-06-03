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
    public class BranchRepositoryTests : RepositoryTestsBase
    {
        private readonly IBranchRepository branchRepository;

        public BranchRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.Branches);

            branchRepository = new BranchRepository(contextMock.Object);
        }

        [Fact]
        public async Task FindAsync_ReturnsCorrectEntity()
        {
            var id = new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f");
            var result = await branchRepository.FindAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.BranchId);
            Assert.Equal("1", result.Title);
            Assert.Equal("11", result.InventoryPart);
        }

        [Fact]
        public async Task FindAsync_ReturnsNull_WhenEntityNotFound()
        {
            var result = await branchRepository.FindAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task FindByInventoryPartAsync_ReturnsCorrectEntity()
        {
            var result = await branchRepository.FindByInventoryPartAsync("22");

            Assert.NotNull(result);
            Assert.Equal(new Guid("568cc092-e24d-4610-8b55-ad5a12f4dada"), result.BranchId);
            Assert.Equal("2", result.Title);
            Assert.Equal("22", result.InventoryPart);
        }

        [Fact]
        public async Task FindByInventoryPartAsync_ReturnsNull_WhenEntityNotFound()
        {
            var result = await branchRepository.FindByInventoryPartAsync("AA");

            Assert.Null(result);
        }

        private static IEnumerable<Branch> GetStubbedData()
        {
            return new List<Branch>()
            {
                new Branch()
                {
                    BranchId = new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f"),
                    Title = "1",
                    InventoryPart = "11",
                },
                new Branch()
                {
                    BranchId = new Guid("568cc092-e24d-4610-8b55-ad5a12f4dada"),
                    Title = "2",
                    InventoryPart = "22",
                },
                new Branch()
                {
                    BranchId = new Guid("31254963-d506-4021-9a28-67f62bf70a25"),
                    Title = "3",
                    InventoryPart = "33",
                },
                new Branch()
                {
                    BranchId = new Guid("a43a32f8-b5c6-464a-b85e-d6f33af8ca48"),
                    Title = "4",
                    InventoryPart = "44",
                },
                new Branch()
                {
                    BranchId = new Guid("ef942fe2-897c-4935-b1da-d77ee1f7f528"),
                    Title = "5",
                    InventoryPart = "55",
                },
            };
        }
    }
}
