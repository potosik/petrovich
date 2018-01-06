using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ClientPassportIdAlreadyExistException : BusinessException
    {
        public ClientPassportIdAlreadyExistException(Guid clientId, string passportId)
            : base(FormatErrorMessage(ErrorCode.UserPassportIdAlreadyExist, passportId, clientId))
        {
        }
    }
}
