using System;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class LogNotFoundException : BusinessException
    {
        public LogNotFoundException(Guid id)
            : base(FormatErrorMessage(ErrorCode.LogNotFound, id))
        {
        }
    }
}
