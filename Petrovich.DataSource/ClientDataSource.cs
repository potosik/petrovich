using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Context;
using Petrovich.DataSource.Mappers;
using Petrovich.Context.DatabaseProcessing;
using Petrovich.DataSource.Queries;
using Petrovich.DataSource.Operations;

namespace Petrovich.DataSource
{
    public class ClientDataSource : IClientDataSource
    {
        private readonly IPetrovichContextFactory contextFactory;
        private readonly IClientMapper clientMapper;

        public ClientDataSource(IPetrovichContextFactory contextFactory, IClientMapper clientMapper)
        {
            this.contextFactory = contextFactory;
            this.clientMapper = clientMapper;
        }

        public async Task<ClientModelCollection> ListAsync(string filter, int pageIndex, int pageSize)
        {
            var query = new ListClientsByFilterQuery(filter, pageIndex, pageSize);
            var dbClients = await DatabaseProcessor<IPetrovichContext>
                .Model(() => contextFactory.CreateContext())
                .QueryAsync(query)
                .ConfigureAwait(false);
            return clientMapper.ToClientModelCollection(dbClients);
        }

        public async Task<ClientModel> FindAsync(Guid clientId)
        {
            var query = new FindClientByIdQuery(clientId);
            var dbClient = await DatabaseProcessor<IPetrovichContext>
                .Model(() => contextFactory.CreateContext())
                .QueryAsync(query)
                .ConfigureAwait(false);
            return clientMapper.ToClientModel(dbClient);
        }

        public async Task<ClientModel> FindAsync(string passportId)
        {
            var query = new FindClientByPassportIdQuery(passportId);
            var dbClient = await DatabaseProcessor<IPetrovichContext>
                .Model(() => contextFactory.CreateContext())
                .QueryAsync(query)
                .ConfigureAwait(false);
            return clientMapper.ToClientModel(dbClient);
        }

        public async Task<ClientModel> CreateAsync(ClientModel client)
        {
            var contextClient = clientMapper.ToContextEntity(client);
            var operation = new CreateClientOperation(contextClient);
            var createdClient = await DatabaseProcessor<IPetrovichContext>
                .Model(() => contextFactory.CreateContext())
                .DoAsync(operation)
                .ConfigureAwait(false);
            return clientMapper.ToClientModel(createdClient);
        }

        public async Task<ClientModel> UpdateAsync(ClientModel client)
        {
            var contextClient = clientMapper.ToContextEntity(client);
            var operation = new UpdateClientOperation(contextClient);
            var createdClient = await DatabaseProcessor<IPetrovichContext>
                .Model(() => contextFactory.CreateContext())
                .DoAsync(operation)
                .ConfigureAwait(false);
            return clientMapper.ToClientModel(createdClient);
        }
    }
}
