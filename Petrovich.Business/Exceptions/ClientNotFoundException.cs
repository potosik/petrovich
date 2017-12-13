using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ClientNotFoundException : BusinessException
    {
        public ClientNotFoundException(Guid id)
            : base(FormatErrorMessage(ErrorCode.ClientNotFound, id))
        {
        }
    }
}
