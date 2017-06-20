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
    public class FullImageDataSourceTests
    {
        private readonly Mock<IFullImageRepository> fullImageRepositoryMock;
        private readonly Mock<IFullImageMapper> fullImageMapperMock;

        private readonly IFullImageDataSource dataSource;

        public FullImageDataSourceTests()
        {
            fullImageRepositoryMock = new Mock<IFullImageRepository>();
            fullImageMapperMock = new Mock<IFullImageMapper>();

            dataSource = new FullImageDataSource(fullImageRepositoryMock.Object, fullImageMapperMock.Object);
        }
        
        [Fact]
        public async Task CreateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            fullImageRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.FullImage>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.CreateAsync(null);
            });
        }

        [Fact]
        public async Task UpdateOrCreateAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            fullImageRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Context.Entities.FullImage());
            fullImageRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Context.Entities.FullImage>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.UpdateOrCreateAsync(null, Guid.NewGuid());
            });
        }

        [Fact]
        public async Task FindAsync_WhenEntityExceptionThrown_ShouldThrowDatabaseOperationException()
        {
            fullImageRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindAsync(Guid.Empty);
            });
        }
    }
}
