using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface ILogDataSource
    {
        Task<Log> FindAsync(Guid id);
        Task<LogCollection> ListAsync(int pageIndex, int pageSize);
        Task WriteLogAsync(Log entity);
    }
}
