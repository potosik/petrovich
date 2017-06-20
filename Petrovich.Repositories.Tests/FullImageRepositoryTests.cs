using Petrovich.Context.Entities;
using Petrovich.Repositories.Concrete;
using Petrovich.Repositories.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Repositories.Tests
{
    public class FullImageRepositoryTests : RepositoryTestsBase
    {
        private readonly IFullImageRepository fullImageRepository;

        public FullImageRepositoryTests()
        {
            var contextMock = CreateContext().MockSet(GetStubbedData().AsQueryable(), c => c.FullImages);

            fullImageRepository = new FullImageRepository(contextMock.Object);
        }

        [Fact]
        public async Task FindAsync_WhenEntityFound_ReturnsCorrectEntity()
        {
            var id = new Guid("b171ce2c-e2df-4c79-bba2-e8c9f1509491");
            var result = await fullImageRepository.FindAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.FullImageId);
        }

        [Fact]
        public async Task FindAsync_WhenEntityNotFound_ReturnsNull()
        {
            var result = await fullImageRepository.FindAsync(Guid.NewGuid());
            Assert.Null(result);
        }
        
        private static IEnumerable<FullImage> GetStubbedData()
        {
            return new List<FullImage>()
            {
                new FullImage()
                {
                    FullImageId = Guid.NewGuid(),
                },
                new FullImage()
                {
                    FullImageId = new Guid("b171ce2c-e2df-4c79-bba2-e8c9f1509491"),
                },
            };
        }
    }
}
