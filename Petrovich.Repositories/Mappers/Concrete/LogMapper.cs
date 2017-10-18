using System.Collections.Generic;
using System.Linq;
using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using Petrovich.Context.Enumerations;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class LogMapper : ILogMapper
    {
        public LogModel ToLogModel(Log log)
        {
            if (log == null)
            {
                return null;
            }

            return new LogModel()
            {
                LogId = log.LogId,
                CorrelationId = log.CorrelationId,
                Severity = (Business.Models.Enumerations.LogSeverityBusiness)((int)log.Severity),
                Message = log.Message,
                StackTrace = log.StackTrace,
                InnerExceptionMessage = log.InnerExceptionMessage,
                CallStack = log.CallStack,

                Created = log.Created,
                CreatedBy = log.CreatedBy,
                Modified = log.Modified,
                ModifiedBy = log.ModifiedBy,
            };
        }

        public LogModelCollection ToLogModelCollection(IEnumerable<Log> logs)
        {
            return new LogModelCollection(logs.Select(item => ToLogModel(item)));
        }

        public Log ToContextLog(LogModel logModel)
        {
            return new Log()
            {
                LogId = logModel.LogId,
                CorrelationId = logModel.CorrelationId,
                Severity = (LogSeverity)((int)logModel.Severity),
                Message = logModel.Message,
                StackTrace = logModel.StackTrace,
                InnerExceptionMessage = logModel.InnerExceptionMessage,
                CallStack = logModel.CallStack,

                Created = logModel.Created,
                CreatedBy = logModel.CreatedBy,
                Modified = logModel.Modified,
                ModifiedBy = logModel.ModifiedBy,
            };
        }
    }
}
