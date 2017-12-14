using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.DataSource.Mappers;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;
using Petrovich.Context.Enumerations;
using Petrovich.Core;

namespace Petrovich.Repositories.DataSources
{
    public class GroupDataSource : IGroupDataSource
    {
        private readonly IGroupRepository groupRepository;
        private readonly IGroupMapper groupMapper;

        public GroupDataSource(IGroupRepository groupRepository, IGroupMapper groupMapper)
        {
            this.groupRepository = groupRepository;
            this.groupMapper = groupMapper;
        }

        public async Task<GroupModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var groups = await groupRepository.ListAsync(pageIndex, pageSize);
                var count = await groupRepository.ListCountAsync();
                var collection = groupMapper.ToGroupModelCollection(groups);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<GroupModel> CreateAsync(GroupModel group)
        {
            try
            {
                var contextGroup = groupMapper.ToContextGroup(group);
                var newGroup = await groupRepository.CreateAsync(contextGroup);
                return groupMapper.ToGroupModel(newGroup);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<GroupModel> FindAsync(Guid id)
        {
            try
            {
                var group = await groupRepository.FindAsync(id);
                return groupMapper.ToGroupModel(group);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<GroupModel> UpdateAsync(GroupModel group)
        {
            try
            {
                var targetGroup = await groupRepository.FindAsync(group.GroupId);

                targetGroup.Title = group.Title;
                targetGroup.InventoryPart = group.InventoryPart;
                targetGroup.BasePrice = group.Price;
                targetGroup.CategoryId = group.CategoryId;

                await groupRepository.UpdateAsync(targetGroup);
                return groupMapper.ToGroupModel(targetGroup);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(GroupModel group)
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

        public async Task<GroupModelCollection> ListByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var groups = await groupRepository.ListByCategoryIdAsync(categoryId);
                return groupMapper.ToGroupModelCollection(groups);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid categoryId)
        {
            try
            {
                var usedInventoryPartNumbers = await groupRepository.ListUsedInventoryPartsAsync(categoryId);
                if (usedInventoryPartNumbers.Count == Constants.GroupInventoryPartMaxCount)
                {
                    return null;
                }

                for (int i = Constants.GroupInventoryPartMinValue; i < Constants.GroupInventoryPartMaxValue; i++)
                {
                    if (!usedInventoryPartNumbers.Contains(i))
                        return i;
                }

                return null;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
