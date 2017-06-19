using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;
using System;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class ProductMapper : IProductMapper
    {
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
                InventoryPart = entity.InventoryPart,

                ImageFull = entity.ImageFull,
                ImageDefault = entity.ImageDefault,
                ImageSmall = entity.ImageSmall,

                BranchTitle = entity.Category?.Branch?.Title,

                CategoryId = entity.CategoryId,
                CategoryTitle = entity.Category?.Title,

                GroupId = entity.GroupId,
                GroupTitle = entity.Group?.Title,

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
                InventoryPart = entity.InventoryPart,

                ImageFull = entity.ImageFull,
                ImageDefault = entity.ImageDefault,
                ImageSmall = entity.ImageSmall,

                CategoryId = entity.CategoryId,
                GroupId = entity.GroupId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
