using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.DataSource.Mappers;
using Petrovich.Repositories.DataSources;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests.DataSources
{
    public class BranchDataSourceTests
    {
        private readonly Mock<IBranchRepository> branchRepositoryMock;
        private readonly Mock<IBranchMapper> branchMapperMock;

        private readonly IBranchDataSource dataSource;

        public BranchDataSourceTests()
        {
            branchRepositoryMock = new Mock<IBranchRepository>();
            branchMapperMock = new Mock<IBranchMapper>();

            dataSource = new BranchDataSource(branchRepositoryMock.Object, branchMapperMock.Object);
        }

        [Fact]
        public async Task ListAsync_WhenEntityExceptionThrown_ShouldThrowDatabseOperationException()
        {
            branchRepositoryMock.Setup(repository => repository.ListAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAsync(1, 1);
            });
        }

        [Fact]
        public async Task FindByInventoryPartAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            branchRepositoryMock.Setup(repository => repository.FindByInventoryPartAsync(It.IsAny<string>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindByInventoryPartAsync(String.Empty);
            });
        }

        [Fact]
        public async Task CreateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            branchRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.Branch>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.CreateAsync(new Business.Models.BranchModel());
            });
        }

        [Fact]
        public async Task UpdateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            branchRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.UpdateAsync(new Business.Models.BranchModel());
            });
        }

        [Fact]
        public async Task DeleteAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            branchRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.DeleteAsync(new Business.Models.BranchModel());
            });
        }

        [Fact]
        public async Task ListAllAsync_WhenEntityExceptionThrown_ShouldThrowDatabseOperationException()
        {
            branchRepositoryMock.Setup(repository => repository.ListAllAsync())
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAllAsync();
            });
        }
    }
}
