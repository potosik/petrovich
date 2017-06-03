using System.Collections.Generic;
using System.Linq;
using Petrovich.Context.Entities;
using Petrovich.Context.Enumerations;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class LogMapper : ILogMapper
    {
        public Business.Models.Log ToBusinessEntity(Log entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Business.Models.Log()
            {
                LogId = entity.LogId,
                CorrelationId = entity.CorrelationId,
                Severity = (Business.Models.Enumerations.LogSeverity)((int)entity.Severity),
                Message = entity.Message,
                StackTrace = entity.StackTrace,
                InnerExceptionMessage = entity.InnerExceptionMessage,
                CallStack = entity.CallStack,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
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
                CorrelationId = entity.CorrelationId,
                Severity = (LogSeverity)((int)entity.Severity),
                Message = entity.Message,
                StackTrace = entity.StackTrace,
                InnerExceptionMessage = entity.InnerExceptionMessage,
                CallStack = entity.CallStack,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                Modified = entity.Modified,
                ModifiedBy = entity.ModifiedBy,
            };
        }
    }
}
