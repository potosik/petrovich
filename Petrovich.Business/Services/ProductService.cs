using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Business.Data;
using Petrovich.Business.Logging;
using Petrovich.Business.Exceptions;

namespace Petrovich.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductDataSource productDataSource;
        private readonly ICategoryDataSource categoryDataSource;
        private readonly IGroupDataSource groupDataSource;

        public ProductService(IProductDataSource productDataSource, ICategoryDataSource categoryDataSource,
            IGroupDataSource groupDataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            this.productDataSource = productDataSource ?? throw new ArgumentNullException(nameof(productDataSource));
            this.categoryDataSource = categoryDataSource ?? throw new ArgumentNullException(nameof(categoryDataSource));
            this.groupDataSource = groupDataSource ?? throw new ArgumentNullException(nameof(groupDataSource));
        }

        public async Task<ProductModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            await logger.LogNoneAsync($"ProductService.ListAsync: listing products (pageIndex: {pageIndex} pageSize: {pageSize}).");
            return await productDataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            if (product == null)
            {
                await logger.LogInformationAsync("ProductService.CreateAsync: product parameter is null.");
                throw new ArgumentNullException(nameof(product));
            }

            await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get category {product.Category.CategoryId}.");
            var category = await categoryDataSource.FindAsync(product.Category.CategoryId);
            if (category == null)
            {
                await logger.LogInformationAsync($"ProductService.CreateAsync: category not found - {product.Category.CategoryId}.");
                throw new CategoryNotFoundException(product.Category.CategoryId);
            }

            if (product.Group != null)
            {
                await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get group {product.Group.GroupId}.");
                var group = await groupDataSource.FindAsync(product.Group.GroupId);
                if (group == null)
                {
                    await logger.LogInformationAsync($"ProductService.CreateAsync: group not found - {product.Group.GroupId}.");
                    throw new GroupNotFoundException(product.Group.GroupId);
                }
            }

            var inventoryPartValue = (int?)null;
            if (product.Group != null)
            {
                await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get inventory number for product for group {product.Group.GroupId}.");
                inventoryPartValue = await productDataSource.GetNewInventoryNumberInGroupAsync(product.Group.GroupId);
                if (!inventoryPartValue.HasValue)
                {
                    await logger.LogNoneAsync($"ProductService.CreateAsync: there are no empty inventory numbers for group {product.Group.GroupId}.");
                    throw new NoGroupProductsSlotsException(product.Group.GroupId);
                }
            }

            if (!inventoryPartValue.HasValue)
            {
                await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get inventory number for product for category {product.Category.CategoryId}.");
                inventoryPartValue = await productDataSource.GetNewInventoryNumberInCategoryAsync(product.Category.CategoryId);
                if (!inventoryPartValue.HasValue)
                {
                    await logger.LogNoneAsync($"ProductService.CreateAsync: there are no empty inventory numbers for category {product.Category.CategoryId}.");
                    throw new NoCategoryProductsSlotsException(product.Category.CategoryId);
                }
            }
            
            product.InventoryPart = inventoryPartValue.Value;
            ValidatePurchasingInformation(product);

            await logger.LogNoneAsync("ProductService.CreateAsync: creating new product.");
            return await productDataSource.CreateAsync(product);
        }

        public async Task<ProductModel> FindAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"ProductService.FindAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"ProductService.FindAsync: trying to get product by id ({id}).");
            var product = await productDataSource.FindAsync(id);
            if (product == null)
            {
                await logger.LogInformationAsync($"ProductService.FindAsync: product not found - {id}.");
                throw new ProductNotFoundException(id);
            }

            return product;
        }

        public async Task<ProductModel> UpdateAsync(ProductModel product)
        {
            if (product.ProductId == Guid.Empty)
            {
                await logger.LogInformationAsync($"ProductService.UpdateAsync: productId is {product.ProductId}.");
                throw new ArgumentOutOfRangeException(nameof(product.ProductId));
            }

            await logger.LogNoneAsync($"ProductService.UpdateAsync: trying to get product by id ({product.ProductId}).");
            var dbProduct = await productDataSource.FindAsync(product.ProductId);
            if (dbProduct == null)
            {
                await logger.LogInformationAsync($"ProductService.UpdateAsync: product not found - {product.ProductId}.");
                throw new ProductNotFoundException(product.ProductId);
            }

            await logger.LogNoneAsync($"ProductService.UpdateAsync: trying to get category by id ({product.Category.CategoryId}).");
            var dbCategory = await categoryDataSource.FindAsync(product.Category.CategoryId);
            if (dbCategory == null)
            {
                await logger.LogInformationAsync($"ProductService.UpdateAsync: category not found - {product.Category.CategoryId}.");
                throw new CategoryNotFoundException(product.Category.CategoryId);
            }

            if (product.Group != null)
            {
                await logger.LogNoneAsync($"ProductService.UpdateAsync: trying to get group by id ({product.Group.GroupId}).");
                var dbGroup = await groupDataSource.FindAsync(product.Group.GroupId);
                if (dbGroup == null)
                {
                    await logger.LogInformationAsync($"ProductService.UpdateAsync: group not found - {product.Group.GroupId}.");
                    throw new GroupNotFoundException(product.Group.GroupId);
                }
            }

            if (product.InventoryPart != dbProduct.InventoryPart)
            {
                await logger.LogInformationAsync($"ProductService.UpdateAsync: product inventory part changed - {product.InventoryPart} / {dbProduct.InventoryPart}.");
                throw new ProductInventoryPartChangedException(product.ProductId);
            }

            ValidatePurchasingInformation(product);

            await logger.LogNoneAsync("ProductService.UpdateAsync: updating product.");
            return await productDataSource.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await logger.LogInformationAsync($"ProductService.DeleteAsync: id parameter is {id}.");
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await logger.LogNoneAsync($"ProductService.DeleteAsync: trying to get product by id ({id}).");
            var product = await productDataSource.FindAsync(id);
            if (product == null)
            {
                await logger.LogInformationAsync($"ProductService.DeleteAsync: product not found - {id}.");
                throw new ProductNotFoundException(id);
            }

            await logger.LogNoneAsync("ProductService.DeleteAsync: deleting product.");
            await productDataSource.DeleteAsync(product);
        }

        public async Task<ProductModelCollection> SearchFastAsync(string query, int count)
        {
            if (String.IsNullOrWhiteSpace(query))
            {
                await logger.LogInformationAsync($"ProductService.SearchFastAsync: search query if null or empty.");
                throw new ArgumentNullException(nameof(query));
            }

            return await productDataSource.SearchFastAsync(query, count);
        }
        
        private void ValidatePurchasingInformation(ProductModel product)
        {
            if (!product.PurchaseYear.HasValue)
            {
                product.PurchaseMonth = null;
            }
        }

        public async Task<ProductModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                await logger.LogInformationAsync($"ProductService.ListByCategoryIdAsync: categoryId parameter is {categoryId}.");
                throw new ArgumentOutOfRangeException(nameof(categoryId));
            }

            await logger.LogNoneAsync($"ProductService.ListByCategoryIdAsync: trying to get category by id ({categoryId}).");
            var category = await categoryDataSource.FindAsync(categoryId);
            if (category == null)
            {
                await logger.LogInformationAsync($"ProductService.ListByCategoryIdAsync: category not found - {categoryId}.");
                throw new CategoryNotFoundException(categoryId);
            }

            await logger.LogNoneAsync($"ProductService.ListByCategoryIdAsync: listing products by category '{category.CategoryId}'");
            return await productDataSource.ListByCategoryIdAsync(category.CategoryId);
        }

        public async Task<ProductModelCollection> ListByGroupIdAsync(Guid groupId)
        {
            if (groupId == Guid.Empty)
            {
                await logger.LogInformationAsync($"ProductService.ListByGroupIdAsync: groupId parameter is {groupId}.");
                throw new ArgumentOutOfRangeException(nameof(groupId));
            }

            await logger.LogNoneAsync($"ProductService.ListByGroupIdAsync: trying to get group by id ({groupId}).");
            var group = await groupDataSource.FindAsync(groupId);
            if (group == null)
            {
                await logger.LogInformationAsync($"ProductService.ListByGroupIdAsync: group not found - {groupId}.");
                throw new GroupNotFoundException(groupId);
            }

            await logger.LogNoneAsync($"ProductService.ListByGroupIdAsync: listing products by category '{group.GroupId}'");
            return await productDataSource.ListByGroupIdAsync(group.GroupId);
        }

        public async Task<ProductModelCollection> ListAsync(IEnumerable<Guid> productIds)
        {
            if (productIds == null)
            {
                await logger.LogInformationAsync($"ProductService.ListAsync: productIds parameter is null.");
                throw new ArgumentNullException(nameof(productIds));
            }

            if (!productIds.Any())
            {
                await logger.LogInformationAsync($"ProductService.ListByGroupIdAsync: productIds collection is empty.");
                throw new ArgumentOutOfRangeException(nameof(productIds));
            }

            await logger.LogNoneAsync($"ProductService.ListAsync: listing products by ids ({String.Join(",", productIds)}).");
            return await productDataSource.ListAsync(productIds);
        }
    }
}
