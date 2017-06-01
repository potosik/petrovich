using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchDataSource dataSource;

        public BranchService(IBranchDataSource dataSource)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
