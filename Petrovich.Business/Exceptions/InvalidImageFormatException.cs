using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class InvalidImageFormatException : BusinessException
    {
        public InvalidImageFormatException(string fileName)
            : base(FormatErrorMessage(ErrorCode.InvalidImageFormat, fileName))
        {
        }
    }
}
