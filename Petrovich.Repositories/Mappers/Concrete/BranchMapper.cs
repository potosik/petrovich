using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class BranchMapper : IBranchMapper
    {
        public Business.Models.Branch ToBusinessEntity(Branch entity)
        {
            return new Business.Models.Branch()
            {
                BranchId = entity.BranchId,
                Title = entity.Title,
                InventoryPart = entity.InventoryPart,
                
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }

        public Business.Models.BranchCollection ToBusinessEntityCollection(IEnumerable<Branch> entities)
        {
            return new Business.Models.BranchCollection(entities.Select(item => ToBusinessEntity(item)));
        }

        public Branch ToContextEntity(Business.Models.Branch entity)
        {
            return new Branch()
            {
                BranchId = entity.BranchId,
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
