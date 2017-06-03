using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ChildGroupsExistsException : BusinessException
    {
        public ChildGroupsExistsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.ChildGroupsExists, id))
        {
        }
    }
}
