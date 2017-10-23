using Petrovich.Business.Data;
using System;
using Petrovich.Business.Models;
using System.Threading.Tasks;
using Petrovich.Business.Logging;
using Petrovich.Core.Performance;

namespace Petrovich.Business.PerformanceCounters
{
    internal class GroupPerformanceCounter : PerformanceCounterBase, IGroupDataSource
    {
        private readonly IGroupDataSource innerDataSource;

        public GroupPerformanceCounter(IGroupDataSource dataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            innerDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<GroupModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListGroups, new { pageIndex, pageSize }))
            {
                return await innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }

        public async Task<GroupModel> CreateAsync(GroupModel group)
        {
            using (new PerformanceMonitor(EventSource.CreateGroup, new { group }))
            {
                return await innerDataSource.CreateAsync(group);
            }
        }

        public async Task<GroupModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindGroupById, new { id }))
            {
                return await innerDataSource.FindAsync(id);
            }
        }

        public async Task<GroupModel> UpdateAsync(GroupModel group)
        {
            using (new PerformanceMonitor(EventSource.UpdateGroup, new { group }))
            {
                return await innerDataSource.UpdateAsync(group);
            }
        }

        public async Task DeleteAsync(GroupModel group)
        {
            using (new PerformanceMonitor(EventSource.DeleteGroup, new { group }))
            {
                await innerDataSource.DeleteAsync(group);
            }
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsGroupsForCategory, new { categoryId }))
            {
                return await innerDataSource.IsExistsForCategoryAsync(categoryId);
            }
        }

        public async Task<GroupModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.ListGroupsByCategoryId, new { categoryId }))
            {
                return await innerDataSource.ListByCategoryIdAsync(categoryId);
            }
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForGroup, new { categoryId }))
            {
                return await innerDataSource.GetNewInventoryNumberAsync(categoryId);
            }
        }
    }
}
