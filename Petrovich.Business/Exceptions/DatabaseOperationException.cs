using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
