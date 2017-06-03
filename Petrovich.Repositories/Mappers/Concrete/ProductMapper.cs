using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;

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
                InventoryPart = entity.InventoryPart,

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
                InventoryPart = entity.InventoryPart,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
