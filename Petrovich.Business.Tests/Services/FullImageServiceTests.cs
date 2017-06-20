using Moq;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Business.Tests.Services
{
    public class FullImageServiceTests
    {
        private readonly Mock<ILoggingService> loggingServiceMock;
        private readonly Mock<IFullImageDataSource> fullImageDataSourceMock;

        private readonly IFullImageService fullImageService;

        public FullImageServiceTests()
        {
            loggingServiceMock = new Mock<ILoggingService>();
            fullImageDataSourceMock = new Mock<IFullImageDataSource>();

            fullImageService = new FullImageService(fullImageDataSourceMock.Object, loggingServiceMock.Object);
        }

        [Fact]
        public async Task FindAsync_WhenFullImageIdIsEmpty_ThrowsArgumentOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            {
                return fullImageService.FindAsync(Guid.Empty);
            });
        }

        [Fact]
        public async Task FindAsync_WhenFullImageNotFound_ThrowsFullImageNotFoundException()
        {
            fullImageDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync((byte[])null);

            await Assert.ThrowsAsync<FullImageNotFoundException>(() =>
            {
                return fullImageService.FindAsync(Guid.NewGuid());
            });
        }

        [Fact]
        public async Task FindAsync_WhenProductFound_ReturnsProduct()
        {
            fullImageDataSourceMock.Setup(dataSource => dataSource.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new byte[0]);

            var result = await fullImageService.FindAsync(Guid.NewGuid());

            Assert.NotNull(result);
        }
    }
}
