using Petrovich.Business.Exceptions;
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

namespace Petrovich.DataSource.Operations
{
    internal class UpdateClientOperation : IDatabaseOperation<IPetrovichContext, Client>
    {
        private readonly Client entity;

        public UpdateClientOperation(Client entity)
        {
            Guard.NotNullArgument(entity, nameof(entity));

            this.entity = entity;
        }

        public async Task<Client> RunAsync(IPetrovichContext model)
        {
            Guard.NotNullArgument(model, nameof(model));

            var client = await model.Clients
                .FirstOrDefaultAsync(item => item.ClientId == entity.ClientId)
                .ConfigureAwait(false);

            if (client == null)
            {
                throw new ClientNotFoundException(entity.ClientId);
            }

            client.FirstName = entity.FirstName;
            client.LastName = entity.LastName;
            client.Address = entity.Address;
            client.Registered = entity.Registered;
            client.PassportId = entity.PassportId;
            client.PassportData = entity.PassportData;
            client.PersonalId = entity.PersonalId;
            client.BirthDate = entity.BirthDate;
            client.PhonesJson = entity.PhonesJson;
            
            await model.SaveChangesAsync().ConfigureAwait(false);

            return client;
        }
    }
}
