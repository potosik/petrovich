using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;
using Petrovich.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.DataSources
{
    public class FullImageDataSource : IFullImageDataSource
    {
        private readonly IFullImageRepository fullImageRepository;
        private readonly IFullImageMapper fullImageMapper;

        public FullImageDataSource(IFullImageRepository fullImageRepository, IFullImageMapper fullImageMapper)
        {
            this.fullImageRepository = fullImageRepository ?? throw new ArgumentNullException(nameof(fullImageRepository));
            this.fullImageMapper = fullImageMapper ?? throw new ArgumentNullException(nameof(fullImageMapper));
        }

        public async Task<Guid> CreateAsync(byte[] content)
        {
            try
            {
                var image = fullImageMapper.CreateContextEntity(content);
                var newImage = await fullImageRepository.CreateAsync(image);
                return newImage.FullImageId;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Guid> UpdateOrCreateAsync(byte[] content, Guid? imageId)
        {
            try
            {
                if (!imageId.HasValue)
                {
                    return await CreateAsync(content);
                }

                var image = await fullImageRepository.FindAsync(imageId.Value);
                if (image == null)
                {
                    return await CreateAsync(content);
                }

                image.Content = content;
                await fullImageRepository.UpdateAsync(image);
                return image.FullImageId;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<byte[]> FindAsync(Guid id)
        {
            try
            {
                var image = await fullImageRepository.FindAsync(id);
                return image?.Content;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
