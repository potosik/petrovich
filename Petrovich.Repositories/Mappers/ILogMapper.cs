using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Collections.Generic;

namespace Petrovich.Repositories.Mappers
{
    public interface ILogMapper
    {
        LogModel ToLogModel(Log log);
        LogModelCollection ToLogModelCollection(IEnumerable<Log> logs);
        Log ToContextLog(LogModel logModel);
    }
}
