﻿using System.Collections.Generic;
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
                Price = entity.BasePrice,
                PriceType = EnumMapper.Map<Context.Enumerations.PriceType, Business.Models.Enumerations.PriceType>(entity.PriceType),
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
                BasePrice = entity.Price,
                PriceType = EnumMapper.Map<Business.Models.Enumerations.PriceType, Context.Enumerations.PriceType>(entity.PriceType),
                CategoryId = entity.CategoryId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
