using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ChildProductsExistsException : BusinessException
    {
        public ChildProductsExistsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.ChildProductsExists, id))
        {
        }
    }
}
