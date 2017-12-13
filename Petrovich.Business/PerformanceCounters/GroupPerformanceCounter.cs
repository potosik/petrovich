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
            innerDataSource = dataSource;
        }

        public Task<GroupModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            using (new PerformanceMonitor(EventSource.ListGroups, new { pageIndex, pageSize }))
            {
                return innerDataSource.ListAsync(pageIndex, pageSize);
            }
        }

        public Task<GroupModel> CreateAsync(GroupModel group)
        {
            using (new PerformanceMonitor(EventSource.CreateGroup, new { group }))
            {
                return innerDataSource.CreateAsync(group);
            }
        }

        public Task<GroupModel> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindGroupById, new { id }))
            {
                return innerDataSource.FindAsync(id);
            }
        }

        public Task<GroupModel> UpdateAsync(GroupModel group)
        {
            using (new PerformanceMonitor(EventSource.UpdateGroup, new { group }))
            {
                return innerDataSource.UpdateAsync(group);
            }
        }

        public Task DeleteAsync(GroupModel group)
        {
            using (new PerformanceMonitor(EventSource.DeleteGroup, new { group }))
            {
                return innerDataSource.DeleteAsync(group);
            }
        }

        public Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.IsExistsGroupsForCategory, new { categoryId }))
            {
                return innerDataSource.IsExistsForCategoryAsync(categoryId);
            }
        }

        public Task<GroupModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.ListGroupsByCategoryId, new { categoryId }))
            {
                return innerDataSource.ListByCategoryIdAsync(categoryId);
            }
        }

        public Task<int?> GetNewInventoryNumberAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.GetNewInventoryNumberForGroup, new { categoryId }))
            {
                return innerDataSource.GetNewInventoryNumberAsync(categoryId);
            }
        }
    }
}
