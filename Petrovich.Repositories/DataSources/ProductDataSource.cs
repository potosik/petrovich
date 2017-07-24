using Petrovich.Business.Data;
using Petrovich.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;
using Petrovich.Core;

namespace Petrovich.Repositories.DataSources
{
    public class ProductDataSource : IProductDataSource
    {
        private readonly IProductRepository productRepository;
        //private readonly IFullImageRepository fullImageRepository;
        private readonly IProductMapper productMapper;
        //private readonly IFullImageMapper fullImageMapper;
        private readonly IFullImageDataSource fullImageDataSource;

        public ProductDataSource(IProductRepository productRepository,/* IFullImageRepository fullImageRepository, */
            IProductMapper productMapper/*, IFullImageMapper fullImageMapper*/, IFullImageDataSource fullImageDataSource)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            //this.fullImageRepository = fullImageRepository ?? throw new ArgumentNullException(nameof(fullImageRepository));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
            //this.fullImageMapper = fullImageMapper ?? throw new ArgumentNullException(nameof(fullImageMapper));
            this.fullImageDataSource = fullImageDataSource ?? throw new ArgumentNullException(nameof(fullImageDataSource));
        }

        public async Task<ProductCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var products = await productRepository.ListAsync(pageIndex, pageSize);
                var count = await productRepository.ListCountAsync();
                var collection = productMapper.ToBusinessEntityCollection(products);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Product> CreateAsync(Product product)
        {
            try
            {
                var contextProduct = productMapper.ToContextEntity(product);
                if (product.ImageFull != null)
                {
                    contextProduct.FullImageId = await fullImageDataSource.CreateAsync(product.ImageFull);
                }

                var newProduct = await productRepository.CreateAsync(contextProduct);
                return productMapper.ToBusinessEntity(newProduct);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Product> FindAsync(Guid id)
        {
            try
            {
                var product = await productRepository.FindAsync(id);
                return productMapper.ToBusinessEntity(product);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            try
            {
                var targetProduct = await productRepository.FindAsync(product.ProductId);

                targetProduct.Title = product.Title;
                targetProduct.Description = product.Description;
                targetProduct.InventoryPart = product.InventoryPart;
                targetProduct.PurchaseYear = product.PurchaseYear;
                targetProduct.PurchaseMonth = product.PurchaseMonth;
                targetProduct.ImageDefault = product.ImageDefault;
                targetProduct.ImageSmall = product.ImageSmall;
                targetProduct.CategoryId = product.CategoryId;
                targetProduct.GroupId = product.GroupId;

                if (product.ImageFull != null)
                {
                    targetProduct.FullImageId = await fullImageDataSource.UpdateOrCreateAsync(product.ImageFull, targetProduct.FullImageId);
                }

                await productRepository.UpdateAsync(targetProduct);
                return productMapper.ToBusinessEntity(targetProduct);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(Product product)
        {
            try
            {
                var targetProduct = await productRepository.FindAsync(product.ProductId);
                await productRepository.DeleteAsync(targetProduct);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            try
            {
                return await productRepository.IsExistsForCategoryAsync(categoryId);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<bool> IsExistsForGroupAsync(Guid groupId)
        {
            try
            {
                return await productRepository.IsExistsForGroupAsync(groupId);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid categoryId)
        {
            try
            {
                var usedInventoryPartNumbers = await productRepository.ListUsedInventoryPartsAsync(categoryId);
                if (usedInventoryPartNumbers.Count == Constants.ProductInventoryPartMaxCount)
                {
                    return null;
                }

                for (int i = Constants.ProductInventoryPartMinValue; i < Constants.ProductInventoryPartMaxValue; i++)
                {
                    if (!usedInventoryPartNumbers.Contains(i))
                        return i;
                }

                return null;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductCollection> SearchFastAsync(string query, int count)
        {
            try
            {
                var a = DateTime.Now.ToString("o");
                var products = await productRepository.SearchFastAsync(query, count);
                var b = DateTime.Now.ToString("o");
                var totalCount = await productRepository.SearchFastCountAsync(query);
                var collection = productMapper.ToBusinessEntityCollection(products);
                collection.TotalCount = totalCount;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
