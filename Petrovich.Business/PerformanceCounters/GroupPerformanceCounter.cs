using Petrovich.Business.Data;
using System;
using Petrovich.Business.Models;
using System.Threading.Tasks;

namespace Petrovich.Business.PerformanceCounters
{
    public class GroupPerformanceCounter : IGroupDataSource
    {
        private readonly IGroupDataSource innerDataSource;

        public GroupPerformanceCounter(IGroupDataSource dataSource)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<GroupCollection> ListAsync()
        {
            return await innerDataSource.ListAsync();
        }

        public async Task<Group> CreateAsync(Group group)
        {
            return await innerDataSource.CreateAsync(group);
        }

        public async Task<Group> FindAsync(Guid id)
        {
            return await innerDataSource.FindAsync(id);
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            return await innerDataSource.UpdateAsync(group);
        }

        public async Task DeleteAsync(Group group)
        {
            await innerDataSource.DeleteAsync(group);
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            return await innerDataSource.IsExistsForCategoryAsync(categoryId);
        }

        public async Task<GroupCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            return await innerDataSource.ListByCategoryIdAsync(categoryId);
        }
    }
}
