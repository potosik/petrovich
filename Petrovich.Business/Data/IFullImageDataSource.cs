using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface IFullImageDataSource
    {
        Task<Guid> CreateAsync(byte[] content);
        Task<Guid> UpdateOrCreateAsync(byte[] content, Guid? imageId);
        Task<byte[]> FindAsync(Guid id);
    }
}
