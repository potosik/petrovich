﻿using System.Collections.Generic;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class CategoryMapper : ICategoryMapper
    {
        public Business.Models.Category ToBusinessEntity(Category entity)
        {
            return new Business.Models.Category()
            {
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                InventoryPart = entity.InventoryPart,
                
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

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}