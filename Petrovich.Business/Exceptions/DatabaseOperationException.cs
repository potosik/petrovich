using System;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class DatabaseOperationException : BusinessException
    {
        public DatabaseOperationException(Exception innerException)
            : base(FormatErrorMessage(ErrorCode.DatabaseInternalError), innerException)
        {
        }
    }
}
