using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Core;
using Petrovich.Repositories.DataSources;
using Petrovich.DataSource.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests.DataSources
{
    public class CategoryDataSourceTests
    {
        private readonly Mock<ICategoryRepository> categoryRepositoryMock;
        private readonly Mock<ICategoryMapper> categoryMapperMock;

        private readonly ICategoryDataSource dataSource;
        
        public CategoryDataSourceTests()
        {
            categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryMapperMock = new Mock<ICategoryMapper>();

            dataSource = new CategoryDataSource(categoryRepositoryMock.Object, categoryMapperMock.Object);
        }

        [Fact]
        public async Task ListAsync_WhenEntityExceptionThrown_ShouldThrowDatabseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.ListAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAsync(1, 1);
            });
        }
        
        [Fact]
        public async Task CreateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.Category>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.CreateAsync(new Business.Models.CategoryModel());
            });
        }

        [Fact]
        public async Task GetNewInventoryNumberAsync_WhenThereAreAvailableSlots_ReturnsItsNumber()
        {
            categoryRepositoryMock.Setup(repository => repository.ListUsedInventoryPartsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<int>() { 1, 2, 3, 5 });

            var result = await dataSource.GetNewInventoryNumberAsync(Guid.Empty);

            Assert.NotNull(result);
            Assert.True(result.HasValue);
            Assert.Equal(4, result.Value);
        }

        [Fact]
        public async Task GetNewInventoryNumberAsync_WhenThereAreNoAvailableSlots_ReturnsNull()
        {
            var fullList = new List<int>();
            for (int i = Constants.CategoryInventoryPartMinValue; i < Constants.CategoryInventoryPartMaxValue; i++)
            {
                fullList.Add(i);
            }

            categoryRepositoryMock.Setup(repository => repository.ListUsedInventoryPartsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(fullList);

            var result = await dataSource.GetNewInventoryNumberAsync(Guid.Empty);

            Assert.Null(result);
            Assert.False(result.HasValue);
        }

        [Fact]
        public async Task GetNewInventoryNumberAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.ListUsedInventoryPartsAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.GetNewInventoryNumberAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task FindAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.UpdateAsync(new Business.Models.CategoryModel());
            });
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.DeleteAsync(new Business.Models.CategoryModel());
            });
        }

        [Fact]
        public async Task ListByBranchIdAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.ListByBranchIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListByBranchIdAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task ListAllAsync_WhenEntityExceptionThrown_ShouldThrowDatabseOperationException()
        {
            categoryRepositoryMock.Setup(repository => repository.ListAllAsync())
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAllAsync();
            });
        }
    }
}
