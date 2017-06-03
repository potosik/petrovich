using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class GroupNotFoundException : BusinessException
    {
        public GroupNotFoundException(Guid id)
            : base(FormatErrorMessage(ErrorCode.GroupNotFound, id))
        {
        }
    }
}
