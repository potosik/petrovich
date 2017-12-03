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
using Petrovich.Context.Enumerations;

namespace Petrovich.Repositories.DataSources
{
    public class ProductDataSource : IProductDataSource
    {
        private readonly IProductRepository productRepository;
        private readonly IProductMapper productMapper;
        private readonly IFullImageDataSource fullImageDataSource;

        public ProductDataSource(IProductRepository productRepository, IProductMapper productMapper, IFullImageDataSource fullImageDataSource)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
            this.fullImageDataSource = fullImageDataSource ?? throw new ArgumentNullException(nameof(fullImageDataSource));
        }

        public async Task<ProductModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var products = await productRepository.ListAsync(pageIndex, pageSize);
                var count = await productRepository.ListCountAsync();
                var collection = productMapper.ToProductModelCollection(products);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            try
            {
                var contextProduct = productMapper.ToContextProduct(product);
                if (product.ImageFull != null)
                {
                    contextProduct.FullImageId = await fullImageDataSource.CreateAsync(product.ImageFull);
                }

                var newProduct = await productRepository.CreateAsync(contextProduct);
                return productMapper.ToProductModel(newProduct);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductModel> FindAsync(Guid id)
        {
            try
            {
                var product = await productRepository.FindAsync(id);
                return productMapper.ToProductModel(product);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            try
            {
                var targetProduct = await productRepository.FindAsync(product.ProductId);

                targetProduct.Title = product.Title;
                targetProduct.Description = product.Description;
                targetProduct.Defects = product.Defects;
                targetProduct.Price = product.Price;
                targetProduct.AssessedValue = product.AssessedValue;
                targetProduct.InventoryPart = product.InventoryPart;
                targetProduct.PurchaseYear = product.PurchaseYear;
                targetProduct.PurchaseMonth = product.PurchaseMonth;
                targetProduct.ImageDefault = product.ImageDefault;
                targetProduct.ImageSmall = product.ImageSmall;
                targetProduct.CategoryId = product.Category.CategoryId;
                targetProduct.GroupId = product.Group?.GroupId;

                if (product.ImageFull != null)
                {
                    targetProduct.FullImageId = await fullImageDataSource.UpdateOrCreateAsync(product.ImageFull, targetProduct.FullImageId);
                }

                await productRepository.UpdateAsync(targetProduct);
                return productMapper.ToProductModel(targetProduct);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(ProductModel product)
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

        public async Task<int?> GetNewInventoryNumberInCategoryAsync(Guid categoryId)
        {
            try
            {
                var usedInventoryPartNumbers = await productRepository.ListUsedInventoryPartsByCategoryAsync(categoryId);
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

        public async Task<int?> GetNewInventoryNumberInGroupAsync(Guid groupId)
        {
            try
            {
                var usedInventoryPartNumbers = await productRepository.ListUsedInventoryPartsByGroupAsync(groupId);
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

        public async Task<ProductModelCollection> SearchFastAsync(string query, int count)
        {
            try
            {
                var products = await productRepository.SearchFastAsync(query, count);
                var totalCount = await productRepository.SearchFastCountAsync(query);
                var collection = productMapper.ToProductModelCollection(products);
                collection.TotalCount = totalCount;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var products = await productRepository.ListByCategoryIdAsync(categoryId);
                var collection = productMapper.ToProductModelCollection(products);
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductModelCollection> ListByGroupIdAsync(Guid groupId)
        {
            try
            {
                var products = await productRepository.ListByGroupIdAsync(groupId);
                var collection = productMapper.ToProductModelCollection(products);
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<ProductModelCollection> ListAsync(IEnumerable<Guid> productIds)
        {
            try
            {
                var products = await productRepository.ListAsync(productIds);
                var collection = productMapper.ToProductModelCollection(products);
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
