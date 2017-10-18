using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface ILogDataSource
    {
        Task<LogModel> FindAsync(Guid id);
        Task<LogModelCollection> ListAsync(int pageIndex, int pageSize);
        Task WriteLogAsync(LogModel entity);
    }
}
