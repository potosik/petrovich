using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupDataSource dataSource;

        public GroupService(IGroupDataSource dataSource)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
