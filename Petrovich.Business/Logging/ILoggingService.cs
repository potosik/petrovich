using Petrovich.Business.Models;
using System;
using System.Threading.Tasks;

namespace Petrovich.Business.Logging
{
    public interface ILoggingService
    {
        Task LogNoneAsync(string message);
        void LogNone(string message);
        void LogPerformanceMetrics(int eventId, string elapsedTime, string method, string arguments);

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

        Task<LogModelCollection> ListAsync(int pageIndex, int pageSize);
        Task<LogModel> FindAsync(Guid id);
    }
}
