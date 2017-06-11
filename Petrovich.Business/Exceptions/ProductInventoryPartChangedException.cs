using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ProductInventoryPartChangedException : BusinessException
    {
        public ProductInventoryPartChangedException(Guid id)
            : base(FormatErrorMessage(ErrorCode.ProductInventoryPartChanged, id))
        {
        }
    }
}
