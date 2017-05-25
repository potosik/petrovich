using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Business.Models;
using Petrovich.Business.Data;

namespace Petrovich.Business.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogDataSource dataSource;

        public LoggingService(ILogDataSource dataSource)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task LogAsync(LogSeverity severity, string message, string stackTrace = null, string innerExceptionMessage = null)
        {
            var logEntity = new Log()
            {
                Severity = severity,
                Message = message,
                StackTrace = stackTrace,
                InnerExceptionMessage = innerExceptionMessage,
            };

            await dataSource.WriteLogAsync(logEntity);
        }

        public async Task LogCriticalAsync(string message)
        {
            await LogAsync(LogSeverity.Critical, message);
        }

        public async Task LogCriticalAsync(Exception ex)
        {
            await LogAsync(LogSeverity.Critical, null, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogCriticalAsync(string message, Exception ex)
        {
            var formattedMessage = FormatMessageWithException(message, ex);
            await LogAsync(LogSeverity.Critical, formattedMessage, ex.StackTrace, ex.InnerException?.Message);
        }

        public async Task LogErrorAsync(string message)
        {
            await LogAsync(LogSeverity.Error, message);
        }

        public async Task LogErrorAsync(Exception ex)
        {
            await LogAsync(LogSeverity.Error, null, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogErrorAsync(string message, Exception ex)
        {
            var formattedMessage = FormatMessageWithException(message, ex);
            await LogAsync(LogSeverity.Error, formattedMessage, ex.StackTrace, ex.InnerException?.Message);
        }

        public async Task LogInformationAsync(string message)
        {
            await LogAsync(LogSeverity.Information, message);
        }

        public async Task LogInformationAsync(Exception ex)
        {
            await LogAsync(LogSeverity.Information, null, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogInformationAsync(string message, Exception ex)
        {
            var formattedMessage = FormatMessageWithException(message, ex);
            await LogAsync(LogSeverity.Information, formattedMessage, ex.Message, ex.InnerException?.Message);
        }

        public async Task LogNoneAsync(string message)
        {
            await LogAsync(LogSeverity.None, message);
        }

        private string FormatMessageWithException(string message, Exception ex)
        {
            return $"{message} | Exception: {ex.Message}";
        }
    }
}
