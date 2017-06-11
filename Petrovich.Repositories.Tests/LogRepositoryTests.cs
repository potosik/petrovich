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
        public async Task FindAsync_WhenEntityFound_ReturnsCorrectEntity()
        {
            var id = new Guid("568cc092-e24d-4610-8b55-ad5a12f4dada");
            var result = await logRepository.FindAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.LogId);
            Assert.Equal(new Guid("19614241-6f59-4392-bfff-52a199b336d3"), result.CorrelationId);
            Assert.Equal(Context.Enumerations.LogSeverity.Information, result.Severity);
            Assert.Equal("Message", result.Message);
            Assert.Equal("Inner exception", result.InnerExceptionMessage);
            Assert.Equal("Stack trace", result.StackTrace);
        }

        [Fact]
        public async Task FindAsync_WhenEntityNotFound_ReturnsNull()
        {
            var result = await logRepository.FindAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task ListAllAsync_ThrowsException()
        {
            await Assert.ThrowsAsync<Exception>(() =>
            {
                return logRepository.ListAllAsync();
            });
        }

        [Fact]
        public async Task ListAsync_WhenEntitiesFound_ReturnsOnlyEntitiesForLastThreeMonths()
        {
            var result = await logRepository.ListAsync(0, 10);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListCountAsync_WhenEntitiesFound_ReturnsItsCount()
        {
            var result = await logRepository.ListCountAsync();

            Assert.Equal(2, result);
        }


        public static IEnumerable<Log> GetStubbedData()
        {
            return new List<Log>()
            {
                new Log()
                {
                    LogId = new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f"),
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.None,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                    Created = DateTime.Now.AddMonths(-1),
                },
                new Log()
                {
                    LogId = new Guid("568cc092-e24d-4610-8b55-ad5a12f4dada"),
                    CorrelationId = new Guid("19614241-6f59-4392-bfff-52a199b336d3"),
                    Severity = Context.Enumerations.LogSeverity.Information,
                    Message = "Message",
                    InnerExceptionMessage = "Inner exception",
                    StackTrace = "Stack trace",
                    Created = DateTime.Now.AddMonths(-2),
                },
                new Log()
                {
                    LogId = new Guid("31254963-d506-4021-9a28-67f62bf70a25"),
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Error,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                    Created = DateTime.Now.AddMonths(-4),
                },
                new Log()
                {
                    LogId = new Guid("a43a32f8-b5c6-464a-b85e-d6f33af8ca48"),
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Error,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
                },
                new Log()
                {
                    LogId = new Guid("ef942fe2-897c-4935-b1da-d77ee1f7f528"),
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
