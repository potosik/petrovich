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
    public class GroupRepositoryTests : RepositoryTestsBase
    {
        private readonly IGroupRepository groupRepository;

        public GroupRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.Groups);

            groupRepository = new GroupRepository(contextMock.Object);
        }

        [Fact]
        public async Task FindAsync_WhenEntityFound_ReturnsCorrectEntity()
        {
            var id = new Guid("4ec51616-aa34-49db-949f-ef45edc9d5fb");
            var result = await groupRepository.FindAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.GroupId);
            Assert.Equal("2", result.Title);
        }

        [Fact]
        public async Task FindAsync_WhenEntityNotFound_ReturnsNull()
        {
            var result = await groupRepository.FindAsync(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact]
        public async Task IsExistsForCategoryAsync_WhenEntitiesFound_ReturnsTrue()
        {
            var result = await groupRepository.IsExistsForCategoryAsync(new Guid("3c494bcf-172f-4e5c-959c-a41718404491"));
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistsForCategoryAsync_WhenEntitiesNotFound_ReturnsFalse()
        {
            var result = await groupRepository.IsExistsForCategoryAsync(Guid.NewGuid());
            Assert.False(result);
        }

        private static IEnumerable<Group> GetStubbedData()
        {
            return new List<Group>()
            {
                new Group()
                {
                    GroupId = Guid.NewGuid(),
                    Title = "1",
                    CategoryId = Guid.NewGuid(),
                },
                new Group()
                {
                    GroupId = new Guid("4ec51616-aa34-49db-949f-ef45edc9d5fb"),
                    Title = "2",
                    CategoryId = new Guid("3c494bcf-172f-4e5c-959c-a41718404491"),
                },
                new Group()
                {
                    GroupId = Guid.NewGuid(),
                    Title = "3",
                    CategoryId = Guid.NewGuid(),
                },
                new Group()
                {
                    GroupId = Guid.NewGuid(),
                    Title = "4",
                    CategoryId = Guid.NewGuid(),
                },
                new Group()
                {
                    GroupId = Guid.NewGuid(),
                    Title = "5",
                    CategoryId = Guid.NewGuid(),
                },
            };
        }
    }
}
