using Petrovich.Business.Models;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface ILogDataSource
    {
        Task<Log> FindAsync(int id);
        Task<LogCollection> ListAsync(int pageIndex, int pageSize);
        Task WriteLogAsync(Log entity);
    }
}
