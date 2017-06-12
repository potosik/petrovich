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
        private readonly IProductMapper productMapper;

        public ProductDataSource(IProductRepository productRepository, IProductMapper productMapper)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
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
                targetProduct.CategoryId = product.CategoryId;
                targetProduct.GroupId = product.GroupId;

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
    }
}
