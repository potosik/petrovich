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

        public async Task<ProductCollection> ListAsync(int pageIndex, int pageSize)
        {
            await logger.LogNoneAsync($"ProductService.ListAsync: listing products (pageIndex: {pageIndex} pageSize: {pageSize}).");
            return await productDataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (product == null)
            {
                await logger.LogInformationAsync("ProductService.CreateAsync: product parameter is null.");
                throw new ArgumentNullException(nameof(product));
            }

            await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get category {product.CategoryId}.");
            var category = await categoryDataSource.FindAsync(product.CategoryId);
            if (category == null)
            {
                await logger.LogInformationAsync($"ProductService.CreateAsync: category not found - {product.CategoryId}.");
                throw new CategoryNotFoundException(product.CategoryId);
            }

            if (product.GroupId.HasValue)
            {
                await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get group {product.GroupId.Value}.");
                var group = await groupDataSource.FindAsync(product.GroupId.Value);
                if (group == null)
                {
                    await logger.LogInformationAsync($"ProductService.CreateAsync: group not found - {product.GroupId.Value}.");
                    throw new GroupNotFoundException(product.GroupId.Value);
                }
            }
            
            await logger.LogNoneAsync($"ProductService.CreateAsync: trying to get inventory number for product for category {product.CategoryId}.");
            var inventoryPartValue = await productDataSource.GetNewInventoryNumberAsync(product.CategoryId);
            if (!inventoryPartValue.HasValue)
            {
                await logger.LogNoneAsync($"ProductService.CreateAsync: there are no empty inventory numbers for category {product.CategoryId}.");
                throw new NoCategoryProductsSlotsException(product.CategoryId);
            }

            product.InventoryPart = inventoryPartValue.Value;
            await logger.LogNoneAsync("ProductService.CreateAsync: creating new product.");
            return await productDataSource.CreateAsync(product);
        }

        public async Task<Product> FindAsync(Guid id)
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

        public async Task<Product> UpdateAsync(Product product)
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

            await logger.LogNoneAsync($"ProductService.UpdateAsync: trying to get category by id ({product.CategoryId}).");
            var dbCategory = await categoryDataSource.FindAsync(product.CategoryId);
            if (dbCategory == null)
            {
                await logger.LogInformationAsync($"ProductService.UpdateAsync: category not found - {product.CategoryId}.");
                throw new CategoryNotFoundException(product.CategoryId);
            }

            if (product.GroupId.HasValue)
            {
                await logger.LogNoneAsync($"ProductService.UpdateAsync: trying to get group by id ({product.GroupId.Value}).");
                var dbGroup = await groupDataSource.FindAsync(product.GroupId.Value);
                if (dbGroup == null)
                {
                    await logger.LogInformationAsync($"ProductService.UpdateAsync: group not found - {product.GroupId.Value}.");
                    throw new GroupNotFoundException(product.GroupId.Value);
                }
            }

            if (product.InventoryPart != dbProduct.InventoryPart)
            {
                await logger.LogInformationAsync($"ProductService.UpdateAsync: product inventory part changed - {product.InventoryPart} / {dbProduct.InventoryPart}.");
                throw new ProductInventoryPartChangedException(product.ProductId);
            }

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
    }
}
