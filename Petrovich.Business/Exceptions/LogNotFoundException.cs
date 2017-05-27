using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
