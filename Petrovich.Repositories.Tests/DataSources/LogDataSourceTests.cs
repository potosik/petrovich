using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Repositories.DataSources;
using Petrovich.DataSource.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests.DataSources
{
    public class LogDataSourceTests
    {
        private readonly Mock<ILogRepository> logRepositoryMock;
        private readonly Mock<ILogMapper> logMapperMock;

        private readonly ILogDataSource dataSource;

        public LogDataSourceTests()
        {
            logRepositoryMock = new Mock<ILogRepository>();
            logMapperMock = new Mock<ILogMapper>();

            dataSource = new LogDataSource(logRepositoryMock.Object, logMapperMock.Object);
        }
        
        [Fact]
        public async Task FindAsync_ThrowsDatabaseOperationException_WhenEntityExceptionThrown()
        {
            logRepositoryMock.Setup(repository => repository.FindAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindAsync(Guid.Empty);
            });
        }
        
        [Fact]
        public async Task ListAsync_ThrowsDatabaseOperationException_WhenEntityExceptionThrown()
        {
            logRepositoryMock.Setup(repository => repository.ListAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAsync(0, 0);
            });
        }

        [Fact]
        public async Task WriteLogAsync_ThrowsDatabaseOperationException_WhenEntityExceptionThrown()
        {
            logRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.Log>()))
                .ThrowsAsync(new EntityException());

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.WriteLogAsync(null);
            });
        }
    }
}
