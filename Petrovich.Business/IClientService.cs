using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business
{
    public interface IClientService
    {
        Task<ClientModelCollection> ListAsync(string filter, int pageIndex, int pageSize);
        Task<ClientModel> FindAsync(Guid id);
        Task<ClientModel> CreateAsync(ClientModel client);
        Task<ClientModel> UpdateAsync(ClientModel client);
    }
}
