using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ProductNotFoundException : BusinessException
    {
        public ProductNotFoundException(Guid id)
            : base(FormatErrorMessage(ErrorCode.ProductNotFound, id))
        {
    }
}
}
