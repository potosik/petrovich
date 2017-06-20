using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class FullImageNotFoundException : BusinessException
    {
        public FullImageNotFoundException(Guid id) 
            : base(FormatErrorMessage(ErrorCode.FullImageNotFound, id))
        {
        }
    }
}
