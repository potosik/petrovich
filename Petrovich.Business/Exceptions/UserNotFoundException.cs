using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
