using System.Collections.Generic;
using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class GroupMapper : IGroupMapper
    {
        public GroupModel ToGroupModel(Group group)
        {
            if (group == null)
            {
                return null;
            }

            return new GroupModel()
            {
                GroupId = group.GroupId,
                Title = group.Title,
                InventoryPart = group.InventoryPart,
                Price = group.BasePrice,
                PriceType = EnumMapper.Map<Context.Enumerations.PriceType, Business.Models.Enumerations.PriceTypeBusiness>(group.PriceType),

                BranchInventoryPart = group.Category.Branch?.InventoryPart,

                CategoryId = group.CategoryId,
                CategoryTitle = group.Category?.Title,
                CategoryInventoryPart = group.Category?.InventoryPart ?? 0,
                
                Created = group.Created,
                CreatedBy = group.CreatedBy,
                Modified = group.Modified,
                ModifiedBy = group.ModifiedBy,
            };
        }

        public GroupModelCollection ToGroupModelCollection(IEnumerable<Group> groups)
        {
            return new GroupModelCollection(groups.Select(item => ToGroupModel(item)));
        }

        public Group ToContextGroup(GroupModel groupModel)
        {
            return new Group()
            {
                GroupId = groupModel.GroupId,
                Title = groupModel.Title,
                InventoryPart = groupModel.InventoryPart,
                BasePrice = groupModel.Price,
                PriceType = EnumMapper.Map<Business.Models.Enumerations.PriceTypeBusiness, Context.Enumerations.PriceType>(groupModel.PriceType),
                CategoryId = groupModel.CategoryId,

                Created = groupModel.Created,
                CreatedBy = groupModel.CreatedBy,
                Modified = groupModel.Modified,
                ModifiedBy = groupModel.ModifiedBy,
            };
        }
    }
}
