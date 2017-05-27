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
        Task LogNoneAsync(string message);
        Task LogInformationAsync(string message);
        Task LogInformationAsync(Exception ex);
        Task LogInformationAsync(string message, Exception ex);
        Task LogErrorAsync(string message);
        Task LogErrorAsync(Exception ex);
        Task LogErrorAsync(string message, Exception ex);
        Task LogCriticalAsync(string message);
        Task LogCriticalAsync(Exception ex);
        Task LogCriticalAsync(string message, Exception ex);
        Task LogAsync(LogSeverity severity, string message, string stackTrace = null, string innerExceptionMessage = null);
    }
}
