using Petrovich.Business.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Logging
{
    public interface ILoggingService
    {
        Task LogNone(string message);
        Task LogInformation(string message);
        Task LogError(string message);
        Task LogError(Exception ex);
        Task LogError(string message, Exception ex);
        Task LogCritical(string message);
        Task LogCritical(Exception ex);
        Task LogCritical(string message, Exception ex);
        Task Log(LogSeverity severity, string message, string stackTrace);
    }
}
