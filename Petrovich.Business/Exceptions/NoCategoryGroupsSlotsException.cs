using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class NoCategoryGroupsSlotsException : BusinessException
    {
        public NoCategoryGroupsSlotsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.NoCategoryGroupsSlots, id))
        {
        }
    }
}
