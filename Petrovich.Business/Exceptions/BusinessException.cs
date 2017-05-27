using Petrovich.Business.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public abstract class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        protected static string GetMessage(ErrorCode code, string errorMessage)
        {
            return $"{(int)code}:{errorMessage}";
        }

        protected static string GetMessage(ErrorCode code)
        {
            var resourceKey = $"E{(int)code}";
            var resourceString = Resources.ResourceManager.GetString(resourceKey);
            return $"{resourceKey}:{resourceString}";
        }

        protected static string FormatErrorMessage(ErrorCode code, params object[] value)
        {
            return String.Format(GetMessage(code), value);
        }
    }

}
