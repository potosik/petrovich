using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface IGroupDataSource
    {
        Task<GroupCollection> ListAsync();
        Task<Group> CreateAsync(Group group);
        Task<Group> FindAsync(Guid id);
        Task<Group> UpdateAsync(Group group);
        Task DeleteAsync(Group group);
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<GroupCollection> ListByCategoryIdAsync(Guid categoryId);
    }
}
