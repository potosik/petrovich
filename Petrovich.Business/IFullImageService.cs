using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business
{
    public interface IFullImageService
    {
        Task<byte[]> FindAsync(Guid id);
    }
}
