using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Context.Entities;
using Petrovich.Context.Enumerations;

namespace Petrovich.Repositories.Mappers
{
    public class LogMapper : ILogMapper
    {
        public Business.Models.Log ToBusinessEntity(Log entity)
        {
            return new Business.Models.Log()
            {
                LogId = entity.LogId,
                Severity = (Business.Models.Enumerations.LogSeverity)((int)entity.Severity),
                Message = entity.Message,
                StackTrace = entity.StackTrace,
                InnerExceptionMessage = entity.InnerExceptionMessage,
                CallStack = entity.CallStack,
            };
        }

        public Business.Models.LogCollection ToBusinessEntityCollection(IEnumerable<Log> entities)
        {
            return new Business.Models.LogCollection(entities.Select(item => ToBusinessEntity(item)));
        }

        public Log ToContextEntity(Business.Models.Log entity)
        {
            return new Log()
            {
                LogId = entity.LogId,
                Severity = (LogSeverity)((int)entity.Severity),
                Message = entity.Message,
                StackTrace = entity.StackTrace,
                InnerExceptionMessage = entity.InnerExceptionMessage,
                CallStack = entity.CallStack,
            };
        }
    }
}
