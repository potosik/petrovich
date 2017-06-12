using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Repositories.DataSources;
using Petrovich.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests.DataSources
{
    public class GroupDataSourceTests
    {
        private readonly Mock<IGroupRepository> groupRepositoryMock;
        private readonly Mock<IGroupMapper> groupMapperMock;

        private readonly IGroupDataSource dataSource;

        public GroupDataSourceTests()
        {
            groupRepositoryMock = new Mock<IGroupRepository>();
            groupMapperMock = new Mock<IGroupMapper>();

            dataSource = new GroupDataSource(groupRepositoryMock.Object, groupMapperMock.Object);
        }

        [Fact]
        public async Task ListAsync_WhenEntityExceptionThrown_ShouldThrowDatabseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.ListAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAsync(1, 1);
            });
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.Group>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.CreateAsync(new Business.Models.Group());
            });
        }

        [Fact]
        public async Task FindAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.UpdateAsync(new Business.Models.Group());
            });
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.DeleteAsync(new Business.Models.Group());
            });
        }

        [Fact]
        public async Task IsExistsForCategoryAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.IsExistsForCategoryAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.IsExistsForCategoryAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task ListByCategoryIdAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            groupRepositoryMock.Setup(repository => repository.ListByCategoryIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListByCategoryIdAsync(Guid.Empty);
            });
        }
    }
}
