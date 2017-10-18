using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Petrovich.Web.Models
{
    public class JsonResponseViewModel
    {
        public bool Success { get; set; }
        public object Result { get; set; }
        public string ErrorMessage { get; set; }

        public JsonResponseViewModel()
            : this(true, null, null)
        {
        }

        public JsonResponseViewModel(string errorMessage)
            : this(false, null, errorMessage)
        {
        }

        public JsonResponseViewModel(object result)
            : this(true, result, null)
        {
        }

        public JsonResponseViewModel(bool success, object result, string errorMessage)
        {
            Success = success;
            Result = result;
            ErrorMessage = errorMessage;
        }

        public static string FormatExceptionMessage(Exception ex)
        {
            var message = new StringBuilder();
            var currentException = ex;
            while (currentException != null)
            {
                message.AppendLine($"Type: {currentException.GetType().Name}");

                if (!String.IsNullOrWhiteSpace(currentException.Message))
                {
                    message.AppendLine(currentException.Message);
                }

                currentException = currentException.InnerException;
            }
            return message.ToString();
        }
    }
}