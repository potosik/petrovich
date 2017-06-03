using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Logging
{
    public class LogViewModel
    {
        [Display(Name = "Идентификатор записи")]
        public Guid LogId { get; set; }

        [Display(Name = "Идентификатор запроса")]
        public Guid CorrelationId { get; set; }

        [Display(Name = "Уровень")]
        public string Severity { get; set; }

        [Display(Name = "Сообщение")]
        public string Message { get; set; }

        [Display(Name = "Stack Trace")]
        public string StackTrace { get; set; }

        [Display(Name = "Inner Exception Message")]
        public string InnerExceptionMessage { get; set; }

        [Display(Name = "Call Stack")]
        public string CallStack { get; set; }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }

        public static LogViewModel Create(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            return new LogViewModel()
            {
                LogId = log.LogId,
                CorrelationId = log.CorrelationId,
                Severity = Properties.Resources.ResourceManager.GetString(log.Severity.ToString()),
                Message = log.Message,
                StackTrace = log.StackTrace,
                InnerExceptionMessage = log.InnerExceptionMessage,
                CallStack = log.CallStack,

                Created = log.Created,
                CreatedBy = log.CreatedBy ?? "System",
            };
        }
    }
}