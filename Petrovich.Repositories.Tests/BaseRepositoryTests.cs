using Petrovich.Context.Entities;
using Petrovich.Repositories.Concrete;
using Petrovich.Repositories.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Context;
using System.Data.Entity;
using Xunit;

namespace Petrovich.Repositories.Tests
{
    public class BaseRepositoryTests : RepositoryTestsBase
    {
        private readonly BaseRepositoryTestClass baseRepository;

        public BaseRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.Logs);

            baseRepository = new BaseRepositoryTestClass(contextMock.Object);
        }
        /*
        [Fact]
        public async Task ListAllAsync_WhenThereAreExistingItems_ReturnsAllItems()
        {
            var result = await baseRepository.ListAllAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task ListAsync_WhenItemsFound_OrdersItemsByCreatedDayDescending()
        {
            var result = await baseRepository.ListAsync(0, 3);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            Assert.Equal(1, result[0].LogId);
            Assert.Equal(4, result[1].LogId);
            Assert.Equal(2, result[2].LogId);
        }

        [Fact]
        public async Task ListAsync_WhenPageSizeIsZero_ReturnsAllItems()
        {
            var resultZeroPageIndex = await baseRepository.ListAsync(0, 0);
            var resultNumberPageIndex = await baseRepository.ListAsync(5, 0);

            Assert.NotNull(resultZeroPageIndex);
            Assert.Equal(5, resultZeroPageIndex.Count);

            Assert.NotNull(resultNumberPageIndex);
            Assert.Equal(5, resultNumberPageIndex.Count);
        }

        [Fact]
        public async Task CreateAsync_WhenCreateSuccessfull_AddsEntityToTable()
        {
            var itemsCountBeforeAdd = (await baseRepository.ListAllAsync()).Count;
            await baseRepository.CreateAsync(new Log());
            var itemsCountAfterAdd = (await baseRepository.ListAllAsync()).Count;

            Assert.Equal(itemsCountBeforeAdd + 1, itemsCountAfterAdd);
        }

        [Fact]
        public async Task CreateAsync_WhenCreateSuccessfull_ReturnsNewItem()
        {
            var result = await baseRepository.CreateAsync(new Log() { LogId = 12345 });

            Assert.NotNull(result);
            Assert.Equal(12345, result.LogId);
        }
        */
        private IEnumerable<Log> GetStubbedData()
        {
            return new List<Log>()
            {
                new Log()
                {
                    LogId = new Guid("eb9f3a64-c2c1-4f50-93d2-92414ad2511f"),
                    Created = new DateTime(1000000),
                },
                new Log()
                {
                    LogId = new Guid("568cc092-e24d-4610-8b55-ad5a12f4dada"),
                    Created = new DateTime(2000000),
                },
                new Log()
                {
                    LogId = new Guid("31254963-d506-4021-9a28-67f62bf70a25"),
                    Created = new DateTime(3000000),
                },
                new Log()
                {
                    LogId = new Guid("a43a32f8-b5c6-464a-b85e-d6f33af8ca48"),
                    Created = new DateTime(1500000),
                },
                new Log()
                {
                    LogId = new Guid("ef942fe2-897c-4935-b1da-d77ee1f7f528"),
                    Created = new DateTime(4000000),
                },
            };
        }

        private class BaseRepositoryTestClass : BaseRepostory<Log>
        {
            public BaseRepositoryTestClass(IPetrovichContext context) 
                : base(context)
            {
            }

            public override async Task<Log> FindAsync(Guid id)
            {
                return await context.Logs.FirstOrDefaultAsync(item => item.LogId == id);
            }
        }
    }
}
