using System;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class LogNotFoundException : BusinessException
    {
        public LogNotFoundException(int id)
            : base(FormatErrorMessage(ErrorCode.LogNotFound, id))
        {
        }
    }
}
