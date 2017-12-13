using Petrovich.Context;
using Petrovich.Context.DatabaseProcessing;
using Petrovich.Context.Entities;
using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.DataSource.Queries
{
    internal class FindClientByIdQuery : IDatabaseQuery<IPetrovichContext, Client>
    {
        private readonly Guid clientId;

        public FindClientByIdQuery(Guid clientId)
        {
            this.clientId = clientId;
        }

        public async Task<Client> ExecuteAsync(IPetrovichContext model)
        {
            Guard.NotNullArgument(model, nameof(model));

            return await model.Clients
                .FirstOrDefaultAsync(item => item.ClientId == clientId)
                .ConfigureAwait(false);
        }
    }
}
