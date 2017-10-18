using System;
using System.Threading.Tasks;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Business.Models;
using Petrovich.Business.Data;
using Petrovich.Business.Exceptions;

namespace Petrovich.Business.Logging
{
    public class LoggingService : ILoggingService
    {
        private const int StackTraceSkip = 6;

        private readonly ILogDataSource dataSource;

        public LoggingService(ILogDataSource dataSource)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        private async Task LogAsync(LogSeverityBusiness severity, string message, string stackTrace = null, string innerExceptionMessage = null)
        {
            var logEntity = new LogModel()
            {
                Severity = severity,
                Message = message,
                StackTrace = stackTrace,
                InnerExceptionMessage = innerExceptionMessage,
                CallStack = new System.Diagnostics.StackTrace(StackTraceSkip).ToString(),
            };

            await dataSource.WriteLogAsync(logEntity);
        }

        public async Task LogCriticalAsync(string message)
        {
            await LogAsync(LogSeverityBusiness.Critical, message);
        }

        public async Task LogCriticalAsync(Exception ex)
        {
            await LogAsync(LogSeverityBusiness.Critical, null, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogCriticalAsync(string message, Exception ex)
        {
            var formattedMessage = FormatMessageWithException(message, ex);
            await LogAsync(LogSeverityBusiness.Critical, formattedMessage, ex.StackTrace, ex.InnerException?.Message);
        }

        public async Task LogErrorAsync(string message)
        {
            await LogAsync(LogSeverityBusiness.Error, message);
        }

        public async Task LogErrorAsync(Exception ex)
        {
            await LogAsync(LogSeverityBusiness.Error, null, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogErrorAsync(string message, Exception ex)
        {
            var formattedMessage = FormatMessageWithException(message, ex);
            await LogAsync(LogSeverityBusiness.Error, formattedMessage, ex.StackTrace, ex.InnerException?.Message);
        }

        public async Task LogInformationAsync(string message)
        {
            await LogAsync(LogSeverityBusiness.Information, message);
        }

        public async Task LogInformationAsync(Exception ex)
        {
            await LogAsync(LogSeverityBusiness.Information, null, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogInformationAsync(string message, Exception ex)
        {
            var formattedMessage = FormatMessageWithException(message, ex);
            await LogAsync(LogSeverityBusiness.Information, formattedMessage, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogNoneAsync(string message)
        {
            await LogAsync(LogSeverityBusiness.None, message);
        }

        public void LogNone(string message)
        {
            Task.Run(async () => await LogAsync(LogSeverityBusiness.None, message)).Wait();
        }

        public async Task LogInvalidModelAsync(Type type)
        {
            await LogInformationAsync($"Invalid model state registered ({ type.FullName }).");
        }

        private string FormatMessageWithException(string message, Exception ex)
        {
            return $"{message} | Exception: {ex.Message}";
        }

        public async Task<LogModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            return await dataSource.ListAsync(pageIndex, pageSize);
        }

        public async Task<LogModel> FindAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var log = await dataSource.FindAsync(id);
            if (log == null)
            {
                throw new LogNotFoundException(id);
            }

            return log;
        }

        public void LogPerformanceMetrics(int eventId, string elapsedTime, string method, string arguments)
        {
            var message = $"METRIC: {elapsedTime} {eventId} {method} {arguments}";
            Task.Run(async () => await LogAsync(LogSeverityBusiness.Performance, message)).Wait();
        }
    }
}
