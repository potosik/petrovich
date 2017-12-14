using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Collections.Generic;

namespace Petrovich.DataSource.Mappers
{
    public interface IGroupMapper
    {
        GroupModel ToGroupModel(Group group);
        GroupModelCollection ToGroupModelCollection(IEnumerable<Group> groups);
        Group ToContextGroup(GroupModel groupModel);
    }
}
