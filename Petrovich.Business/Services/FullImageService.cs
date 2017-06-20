using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Services
{
    public class FullImageService : BaseService, IFullImageService
    {
        private readonly IFullImageDataSource fullImageDataSource;

        public FullImageService(IFullImageDataSource fullImageDataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            this.fullImageDataSource = fullImageDataSource ?? throw new ArgumentNullException(nameof(fullImageDataSource));
        }

        public async Task<byte[]> FindAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync("FullImageService.FindAsync: id parameter is empty.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"FullImageService.FindAsync: trying to get image {id}.");
            var image = await fullImageDataSource.FindAsync(id);
            if (image == null)
            {
                await logger.LogInformationAsync($"FullImageService.FindAsync: image not found - {id}.");
                throw new FullImageNotFoundException(id);
            }

            return image;
        }
    }
}
