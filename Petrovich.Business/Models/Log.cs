using Petrovich.Business.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public LogSeverity Severity { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string CallStack { get; set; }
    }
}
