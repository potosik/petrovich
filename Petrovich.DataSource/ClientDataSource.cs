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

        public async Task<ClientModelCollection> ListAsync(string filter)
        {
            var query = new ListClientsByFilterQuery(filter);
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
    }
}
