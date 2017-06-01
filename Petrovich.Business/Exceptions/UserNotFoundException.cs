using System;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class UserNotFoundException : BusinessException
    {
        public UserNotFoundException(string id)
            : base(FormatErrorMessage(ErrorCode.UserNotFound, id))
        {
        }
    }
}
