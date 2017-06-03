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

        //[Fact]
        //public async Task ListAsync_WhenEntitiesFound_ReturnsList()
        //{
        //    var fullPage = await logRepository.ListAsync(1, 2);
        //    var notFullPage = await logRepository.ListAsync(1, 4);

        //    Assert.NotNull(fullPage);
        //    Assert.NotNull(notFullPage);
        //    Assert.Equal(2, fullPage.Count);
        //    Assert.Equal(1, notFullPage.Count);
        //}

        //[Fact]
        //public async Task ListAsync_WhenEntitiesNotFound_ReturnsEmptyList()
        //{
        //    var result = await logRepository.ListAsync(50, 10);

        //    Assert.NotNull(result);
        //    Assert.Equal(0, result.Count);
        //}

        //[Fact]
        //public async Task ListAsync_WhenItemsFound_OrdersItemsByLogIdDescending()
        //{
        //    var result = await logRepository.ListAsync(0, 10);

        //    Assert.NotNull(result);
        //    Assert.Equal(5, result.Count);

        //    Assert.Equal(5, result[0].LogId);
        //    Assert.Equal(4, result[1].LogId);
        //    Assert.Equal(3, result[2].LogId);
        //    Assert.Equal(2, result[3].LogId);
        //    Assert.Equal(1, result[4].LogId);
        //}

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
                },
                new Log()
                {
                    LogId = new Guid("568cc092-e24d-4610-8b55-ad5a12f4dada"),
                    CorrelationId = new Guid("19614241-6f59-4392-bfff-52a199b336d3"),
                    Severity = Context.Enumerations.LogSeverity.Information,
                    Message = "Message",
                    InnerExceptionMessage = "Inner exception",
                    StackTrace = "Stack trace",
                },
                new Log()
                {
                    LogId = new Guid("31254963-d506-4021-9a28-67f62bf70a25"),
                    CorrelationId = Guid.NewGuid(),
                    Severity = Context.Enumerations.LogSeverity.Error,
                    Message = String.Empty,
                    InnerExceptionMessage = String.Empty,
                    StackTrace = String.Empty,
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
