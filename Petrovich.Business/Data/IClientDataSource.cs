using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Data
{
    public interface IClientDataSource
    {
        Task<ClientModelCollection> ListAsync(string filter);
        Task<ClientModel> FindAsync(Guid clientId);
    }
}
