using System.Collections.Generic;
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

        public Business.Models.Product ToBusinessEntity(Product entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Business.Models.Product()
            {
                ProductId = entity.ProductId,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                PriceType = EnumMapper.Map<Context.Enumerations.PriceType, Business.Models.Enumerations.PriceType>(entity.PriceType),
                InventoryPart = entity.InventoryPart,

                PurchaseYear = entity.PurchaseYear,
                PurchaseMonth = entity.PurchaseMonth,

                ImageFullId = entity.Image?.FullImageId,
                ImageFull = entity.Image?.Content,

                ImageDefault = entity.ImageDefault,
                ImageSmall = entity.ImageSmall,

                BranchId = entity.Category != null ? entity.Category.BranchId : Guid.Empty,
                BranchTitle = entity.Category?.Branch?.Title,

                Category = categoryMapper.ToBusinessEntity(entity.Category),
                Group = groupMapper.ToBusinessEntity(entity.Group),

                BranchInventoryPart = entity.Category?.Branch?.InventoryPart,
                CategoryInventoryPart = entity.Category?.InventoryPart ?? 0,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }

        public Business.Models.ProductCollection ToBusinessEntityCollection(IEnumerable<Product> entities)
        {
            return new Business.Models.ProductCollection(entities.Select(item => ToBusinessEntity(item)));
        }

        public Product ToContextEntity(Business.Models.Product entity)
        {
            return new Product()
            {
                ProductId = entity.ProductId,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                PriceType = EnumMapper.Map<Business.Models.Enumerations.PriceType, Context.Enumerations.PriceType>(entity.PriceType),
                InventoryPart = entity.InventoryPart,

                PurchaseYear = entity.PurchaseYear,
                PurchaseMonth = entity.PurchaseMonth,

                FullImageId = entity.ImageFullId,
                ImageDefault = entity.ImageDefault,
                ImageSmall = entity.ImageSmall,

                CategoryId = entity.Category.CategoryId,
                GroupId = entity.Group?.GroupId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
