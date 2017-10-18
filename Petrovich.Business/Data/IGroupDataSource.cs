using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface IGroupDataSource
    {
        Task<GroupModelCollection> ListAsync(int pageIndex, int pageSize);
        Task<GroupModel> CreateAsync(GroupModel group);
        Task<GroupModel> FindAsync(Guid id);
        Task<GroupModel> UpdateAsync(GroupModel group);
        Task DeleteAsync(GroupModel group);
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<GroupModelCollection> ListByCategoryIdAsync(Guid categoryId);
    }
}
