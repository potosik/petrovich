using System.Collections.Generic;
using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Linq;
using System;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class ProductMapper : IProductMapper
    {
        private readonly ICategoryMapper categoryMapper;
        private readonly IGroupMapper groupMapper;

        public ProductMapper(ICategoryMapper categoryMapper, IGroupMapper groupMapper)
        {
            this.categoryMapper = categoryMapper;
            this.groupMapper = groupMapper;
        }

        public ProductModel ToProductModel(Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new ProductModel()
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Defects = product.Defects,
                Price = product.Price,
                AssessedValue = product.AssessedValue,
                InventoryPart = product.InventoryPart,

                PurchaseYear = product.PurchaseYear,
                PurchaseMonth = product.PurchaseMonth,

                ImageFullId = product.Image?.FullImageId,
                ImageFull = product.Image?.Content,

                ImageDefault = product.ImageDefault,
                ImageSmall = product.ImageSmall,

                BranchId = product.Category != null ? product.Category.BranchId : Guid.Empty,
                BranchTitle = product.Category?.Branch?.Title,

                Category = categoryMapper.ToCategoryModel(product.Category),
                Group = groupMapper.ToGroupModel(product.Group),

                BranchInventoryPart = product.Category?.Branch?.InventoryPart,
                CategoryInventoryPart = product.Category?.InventoryPart ?? 0,
                GroupInventoryPart = product.Group?.InventoryPart ?? 0,

                Created = product.Created,
                CreatedBy = product.CreatedBy,
                Modified = product.Modified,
                ModifiedBy = product.ModifiedBy,
            };
        }

        public ProductModelCollection ToProductModelCollection(IEnumerable<Product> products)
        {
            return new ProductModelCollection(products.Select(item => ToProductModel(item)));
        }

        public Product ToContextProduct(ProductModel productModel)
        {
            return new Product()
            {
                ProductId = productModel.ProductId,
                Title = productModel.Title,
                Description = productModel.Description,
                Defects = productModel.Defects,
                Price = productModel.Price,
                AssessedValue = productModel.AssessedValue,
                InventoryPart = productModel.InventoryPart,

                PurchaseYear = productModel.PurchaseYear,
                PurchaseMonth = productModel.PurchaseMonth,

                FullImageId = productModel.ImageFullId,
                ImageDefault = productModel.ImageDefault,
                ImageSmall = productModel.ImageSmall,

                CategoryId = productModel.Category.CategoryId,
                GroupId = productModel.Group?.GroupId,

                Created = productModel.Created,
                CreatedBy = productModel.CreatedBy,
                Modified = productModel.Modified,
                ModifiedBy = productModel.ModifiedBy,
            };
        }
    }
}
