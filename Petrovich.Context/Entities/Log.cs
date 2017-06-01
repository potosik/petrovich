using Petrovich.Context.Entities.Base;
using Petrovich.Context.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Petrovich.Context.Entities
{
    public class Log : BaseEntity
    {
        [Key]
        public int LogId { get; set; }
        public Guid CorrelationId { get; set; }
        public LogSeverity Severity { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string CallStack { get; set; }
    }
}
