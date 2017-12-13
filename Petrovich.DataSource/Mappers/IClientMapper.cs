using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.DataSource.Mappers
{
    public interface IClientMapper
    {
        ClientModel ToClientModel(Client client);
        ClientModelCollection ToClientModelCollection(IEnumerable<Client> clients);
        Client ToContextEntity(ClientModel clientModel);
    }
}
