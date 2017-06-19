using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Exceptions
{
    [Serializable]
    public class ImageNotFoundException : BusinessException
    {
        public ImageNotFoundException(ObjectType type, Guid id) 
            : base(FormatErrorMessage(ErrorCode.ImageNotFound, type, id))
        {
        }

        public enum ObjectType
        {
            Product,
        }
    }
}
