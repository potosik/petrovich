using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using System;

namespace Petrovich.Business.Models
{
    public class LogModel : BaseEntityModel
    {
        public Guid LogId { get; set; }
        public Guid CorrelationId { get; set; }
        public LogSeverityBusiness Severity { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string CallStack { get; set; }
    }
}
