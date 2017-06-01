using Moq;
using Petrovich.Business.Exceptions;
using Petrovich.Repositories.DataSources;
using Petrovich.Repositories.Mappers;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests.DataSources
{
    public class LogDataSourceTests
    {
        [Fact]
        public async Task FindAsync_ThrowsLogNotFoundException_WhenLogNotFound()
        {
            var logRepository = new Mock<ILogRepository>();
            var logMapper = new Mock<ILogMapper>();

            var dataSource = new LogDataSource(logRepository.Object, logMapper.Object);

            await Assert.ThrowsAsync<LogNotFoundException>(() =>
            {
                return dataSource.FindAsync(0);
            });
        }

        [Fact]
        public async Task FindAsync_ThrowsDatabaseOperationException_WhenEntityExceptionThrown()
        {
            var logRepository = new Mock<ILogRepository>();
            var logMapper = new Mock<ILogMapper>();

            logRepository.Setup(repository => repository.FindAsync(It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            var dataSource = new LogDataSource(logRepository.Object, logMapper.Object);

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.FindAsync(0);
            });
        }

        [Fact]
        public async Task FindAsync_ReturnsMappedLogEntity_WhenEntityFound()
        {
            var logRepository = new Mock<ILogRepository>();
            var logMapper = new Mock<ILogMapper>();

            logRepository.Setup(repository => repository.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(new Context.Entities.Log());
            logMapper.Setup(mapper => mapper.ToBusinessEntity(It.IsAny<Context.Entities.Log>()))
                .Returns(new Business.Models.Log() { LogId = 0 });

            var dataSource = new LogDataSource(logRepository.Object, logMapper.Object);
            var result = await dataSource.FindAsync(0);

            Assert.NotNull(result);
            Assert.Equal(0, result.LogId);
        }

        [Fact]
        public async Task ListAsync_ReturnsCollection_WhenItemsFound()
        {
            var logRepository = new Mock<ILogRepository>();
            var logMapper = new Mock<ILogMapper>();

            logRepository.Setup(repository => repository.ListAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Context.Entities.Log>());
            logMapper.Setup(mapper => mapper.ToBusinessEntityCollection(It.IsAny<IEnumerable<Context.Entities.Log>>()))
                .Returns(new Business.Models.LogCollection());

            var dataSource = new LogDataSource(logRepository.Object, logMapper.Object);
            var result = await dataSource.ListAsync(0, 0);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task ListAsync_ThrowsDatabaseOperationException_WhenEntityExceptionThrown()
        {
            var logRepository = new Mock<ILogRepository>();
            var logMapper = new Mock<ILogMapper>();

            logRepository.Setup(repository => repository.ListAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new EntityException());

            var dataSource = new LogDataSource(logRepository.Object, logMapper.Object);

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.ListAsync(0, 0);
            });
        }

        [Fact]
        public async Task WriteLogAsync_ThrowsDatabaseOperationException_WhenEntityExceptionThrown()
        {
            var logRepository = new Mock<ILogRepository>();
            var logMapper = new Mock<ILogMapper>();

            logRepository.Setup(repository => repository.CreateAsync(It.IsAny<Context.Entities.Log>()))
                .ThrowsAsync(new EntityException());

            var dataSource = new LogDataSource(logRepository.Object, logMapper.Object);

            await Assert.ThrowsAsync<DatabaseOperationException>(() =>
            {
                return dataSource.WriteLogAsync(null);
            });
        }
    }
}
