using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Context.Entities;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class FullImageMapper : IFullImageMapper
    {
        public FullImage CreateContextEntity(byte[] content)
        {
            return new FullImage()
            {
                Content = content,
            };
        }
    }
}
