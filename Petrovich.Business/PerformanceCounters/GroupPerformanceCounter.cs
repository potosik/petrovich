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

        public async Task<GroupCollection> ListAsync()
        {
            using (new PerformanceMonitor(EventSource.ListGroups))
            {
                return await innerDataSource.ListAsync();
            }
        }

        public async Task<Group> CreateAsync(Group group)
        {
            using (new PerformanceMonitor(EventSource.CreateGroup, new { group }))
            {
                return await innerDataSource.CreateAsync(group);
            }
        }

        public async Task<Group> FindAsync(Guid id)
        {
            using (new PerformanceMonitor(EventSource.FindGroupById, new { id }))
            {
                return await innerDataSource.FindAsync(id);
            }
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            using (new PerformanceMonitor(EventSource.UpdateGroup, new { group }))
            {
                return await innerDataSource.UpdateAsync(group);
            }
        }

        public async Task DeleteAsync(Group group)
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

        public async Task<GroupCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            using (new PerformanceMonitor(EventSource.ListGroupsByCategoryId, new { categoryId }))
            {
                return await innerDataSource.ListByCategoryIdAsync(categoryId);
            }
        }
    }
}
