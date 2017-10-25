using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class NoGroupProductsSlotsException : BusinessException
    {
        public NoGroupProductsSlotsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.NoGroupProductsSlots, id))
        {
        }
    }
}
