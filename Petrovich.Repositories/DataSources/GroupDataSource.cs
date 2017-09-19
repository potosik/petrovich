using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Repositories.Mappers;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;
using Petrovich.Context.Enumerations;

namespace Petrovich.Repositories.DataSources
{
    public class GroupDataSource : IGroupDataSource
    {
        private readonly IGroupRepository groupRepository;
        private readonly IGroupMapper groupMapper;

        public GroupDataSource(IGroupRepository groupRepository, IGroupMapper groupMapper)
        {
            this.groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            this.groupMapper = groupMapper ?? throw new ArgumentNullException(nameof(groupMapper));
        }

        public async Task<GroupCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var groups = await groupRepository.ListAsync(pageIndex, pageSize);
                var count = await groupRepository.ListCountAsync();
                var collection = groupMapper.ToBusinessEntityCollection(groups);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Group> CreateAsync(Group group)
        {
            try
            {
                var contextGroup = groupMapper.ToContextEntity(group);
                var newGroup = await groupRepository.CreateAsync(contextGroup);
                return groupMapper.ToBusinessEntity(newGroup);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Group> FindAsync(Guid id)
        {
            try
            {
                var group = await groupRepository.FindAsync(id);
                return groupMapper.ToBusinessEntity(group);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            try
            {
                var targetGroup = await groupRepository.FindAsync(group.GroupId);

                targetGroup.Title = group.Title;
                targetGroup.BasePrice = group.BasePrice;
                targetGroup.PriceType = (PriceType)((int)group.PriceType);
                targetGroup.CategoryId = group.CategoryId;

                await groupRepository.UpdateAsync(targetGroup);
                return groupMapper.ToBusinessEntity(targetGroup);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(Group group)
        {
            try
            {
                var targetGroup = await groupRepository.FindAsync(group.GroupId);
                await groupRepository.DeleteAsync(targetGroup);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            try
            {
                return await groupRepository.IsExistsForCategoryAsync(categoryId);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<GroupCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var groups = await groupRepository.ListByCategoryIdAsync(categoryId);
                return groupMapper.ToBusinessEntityCollection(groups);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
