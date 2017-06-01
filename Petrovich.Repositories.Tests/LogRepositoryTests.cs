using Petrovich.Context.Entities;
using Petrovich.Repositories.Concrete;
using Petrovich.Repositories.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests
{
    public class LogRepositoryTests : RepositoryTestsBase
    {
        private readonly ILogRepository logRepository;

        public LogRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.Logs);

            logRepository = new LogRepository(contextMock.Object);
        }
        
        [Fact]
        public async Task FindAsync_ReturnsCorrectEntity()
        {
            var result = await logRepository.FindAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.LogId);
        }

        [Fact]
        public async Task FindAsync_ReturnsNull_WhenEntityNotFound()
        {
            var result = await logRepository.FindAsync(50);

            Assert.Null(result);
        }

        [Fact]
        public async Task ListAsync_ReturnsList_WhenEntitiesFound()
        {
            var fullPage = await logRepository.ListAsync(1, 2);
            var notFullPage = await logRepository.ListAsync(1, 4);

            Assert.NotNull(fullPage);
            Assert.NotNull(notFullPage);
            Assert.Equal(2, fullPage.Count);
            Assert.Equal(1, notFullPage.Count);
        }

        [Fact]
        public async Task ListAsync_ReturnsEmptyList_WhenEntitiesNotFound()
        {
            var result = await logRepository.ListAsync(50, 10);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        public static IEnumerable<Log> GetStubbedData()
        {
            return new List<Log>()
            {
                new Log()
                {
                    LogId = 0,
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.None,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                },
                new Log()
                {
                    LogId = 1,
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Information,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                },
                new Log()
                {
                    LogId = 2,
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Error,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                },
                new Log()
                {
                    LogId = 3,
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Error,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                },
                new Log()
                {
                    LogId = 4,
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Critical,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                },
            };
        }
    }
}
