using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class GroupInventoryPartChangedException: BusinessException
    {
        public GroupInventoryPartChangedException(Guid id)
            : base(FormatErrorMessage(ErrorCode.GroupInventoryPartChanged, id))
        {
        }
    }
}
