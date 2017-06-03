using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ChildCategoriesExistsException : BusinessException
    {
        public ChildCategoriesExistsException(Guid id)
            : base(FormatErrorMessage(ErrorCode.ChildCategoriesExists, id))
        {
        }
    }
}
