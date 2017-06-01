using Petrovich.Context.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface ILogRepository : IBaseRepository<Log>
    {
        Task<IList<Log>> ListAsync(int pageIndex, int pageSize);
    }
}
