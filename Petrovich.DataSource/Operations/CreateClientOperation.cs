using Petrovich.Business.Models;
using Petrovich.Context;
using Petrovich.Context.DatabaseProcessing;
using Petrovich.Context.Entities;
using Petrovich.Core;
using Petrovich.DataSource.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.DataSource.Operations
{
    internal class CreateClientOperation : IDatabaseOperation<IPetrovichContext, Client>
    {
        private readonly Client entity;

        public CreateClientOperation(Client entity)
        {
            Guard.NotNullArgument(entity, nameof(entity));

            this.entity = entity;
        }

        public async Task<Client> RunAsync(IPetrovichContext model)
        {
            Guard.NotNullArgument(model, nameof(model));
            model.Clients.Add(entity);
            await model.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
