using Petrovich.Business.Models;
using Petrovich.Business.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
