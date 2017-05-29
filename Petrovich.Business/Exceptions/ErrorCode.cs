using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    public enum ErrorCode
    {
        Unknown = 0,
        DatabaseInternalError = 1,

        LogNotFound = 404001,
        UserNotFound = 404002,
    }

}
