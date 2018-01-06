using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Context.Entities;

namespace Petrovich.DataSource.Mappers.Concrete
{
    public class ClientMapper : IClientMapper
    {
        public ClientModel ToClientModel(Client client)
        {
            if (client == null)
            {
                return null;
            }

            return new ClientModel()
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Address = client.Address,
                Registered = client.Registered,
                PassportId = client.PassportId,
                PassportData = client.PassportData,
                PersonalId = client.PersonalId,
                BirthDate = client.BirthDate,
                PhonesJson = client.PhonesJson,

                Created = client.Created,
                CreatedBy = client.CreatedBy,
                Modified = client.Modified,
                ModifiedBy = client.ModifiedBy,
            };
        }

        public ClientModelCollection ToClientModelCollection(IEnumerable<Client> clients)
        {
            return new ClientModelCollection(clients.Select(item => ToClientModel(item)));
        }

        public Client ToContextEntity(ClientModel clientModel)
        {
            return new Client()
            {
                ClientId = clientModel.ClientId,
                FirstName = clientModel.FirstName,
                LastName = clientModel.LastName,
                Address = clientModel.Address,
                Registered = clientModel.Registered,
                PassportId = clientModel.PassportId,
                PassportData = clientModel.PassportData,
                PersonalId = clientModel.PersonalId,
                BirthDate = clientModel.BirthDate,
                PhonesJson = clientModel.PhonesJson,

                Created = clientModel.Created,
                CreatedBy = clientModel.CreatedBy,
                Modified = clientModel.Modified,
                ModifiedBy = clientModel.ModifiedBy,
            };
        }
    }
}
