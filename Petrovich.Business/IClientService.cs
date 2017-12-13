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
        Task<ClientModelCollection> ListAsync(string filter);
        Task<ClientModel> FindAsync(Guid id);
    }
}
