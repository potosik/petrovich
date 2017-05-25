using Petrovich.Context.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.Entities
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public LogSeverity Severity { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
