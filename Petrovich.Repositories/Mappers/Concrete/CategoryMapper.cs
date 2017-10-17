using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class CategoryMapper : ICategoryMapper
    {
        public Business.Models.Category ToBusinessEntity(Category entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Business.Models.Category()
            {
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                InventoryPart = entity.InventoryPart,
                BasePrice = entity.BasePrice,
                PriceType = EnumMapper.Map<Context.Enumerations.PriceType, Business.Models.Enumerations.PriceType>(entity.PriceType),
                BranchId = entity.BranchId,
                BranchTitle = entity.Branch?.Title,
                BranchInventoryPart = entity.Branch?.InventoryPart,
                
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }

        public Business.Models.CategoryCollection ToBusinessEntityCollection(IEnumerable<Category> entities)
        {
            return new Business.Models.CategoryCollection(entities.Select(item => ToBusinessEntity(item)));
        }

        public Category ToContextEntity(Business.Models.Category entity)
        {
            return new Category()
            {
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                InventoryPart = entity.InventoryPart,
                BasePrice = entity.BasePrice,
                PriceType = EnumMapper.Map<Business.Models.Enumerations.PriceType, Context.Enumerations.PriceType>(entity.PriceType),
                BranchId = entity.BranchId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
