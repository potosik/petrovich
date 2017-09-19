using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class GroupMapper : IGroupMapper
    {
        public Business.Models.Group ToBusinessEntity(Group entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Business.Models.Group()
            {
                GroupId = entity.GroupId,
                Title = entity.Title,
                BasePrice = entity.BasePrice,
                PriceType = (Business.Models.Enumerations.PriceType)((int)entity.PriceType),
                CategoryId = entity.CategoryId,

                CategoryTitle = entity.Category?.Title,
                
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
                BasePrice = entity.BasePrice,
                PriceType = (Context.Enumerations.PriceType)((int)entity.PriceType),
                CategoryId = entity.CategoryId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
