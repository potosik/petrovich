﻿using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<IList<Group>> ListByCategoryIdAsync(Guid categoryId);
        Task<IList<int>> ListUsedInventoryPartsAsync(Guid categoryId);
    }
}
