using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class NoCategoryProductsSlotsException : BusinessException
    {
        public NoCategoryProductsSlotsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.NoCategoryProductsSlots, id))
        {
        }
    }
}
