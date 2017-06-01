using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Logging
{
    public interface ILoggingService
    {
        Task LogNoneAsync(string message);
        void LogNone(string v);

        Task LogInformationAsync(string message);
        Task LogInformationAsync(Exception ex);
        Task LogInformationAsync(string message, Exception ex);
        Task LogErrorAsync(string message);
        Task LogErrorAsync(Exception ex);
        Task LogErrorAsync(string message, Exception ex);
        Task LogCriticalAsync(string message);
        Task LogCriticalAsync(Exception ex);
        Task LogCriticalAsync(string message, Exception ex);
        
        Task LogInvalidModelAsync(Type type);
    }
}
