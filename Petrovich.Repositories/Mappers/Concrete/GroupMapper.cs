using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class GroupMapper : IGroupMapper
    {
        public Business.Models.Group ToBusinessEntity(Group entity)
        {
            return new Business.Models.Group()
            {
                GroupId = entity.GroupId,
                Title = entity.Title,
                
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }

        public Business.Models.GroupCollection ToBusinessEntityCollection(IEnumerable<Group> entities)
        {
            return new Business.Models.GroupCollection(entities.Select(item => ToBusinessEntity(item)));
        }

        public Group ToContextEntity(Business.Models.Group entity)
        {
            return new Group()
            {
                GroupId = entity.GroupId,
                Title = entity.Title,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
