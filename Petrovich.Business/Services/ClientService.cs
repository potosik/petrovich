using Petrovich.Business.Data;
using Petrovich.Business.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Business.Exceptions;
using Petrovich.Core;

namespace Petrovich.Business.Services
{
    public class ClientService : BaseService, IClientService
    {
        private readonly IClientDataSource clientDataSource;

        public ClientService(IClientDataSource clientDataSource, ILoggingService loggingService)
            : base(loggingService)
        {
            this.clientDataSource = clientDataSource;
        }

        public async Task<ClientModelCollection> ListAsync(string filter)
        {
            await logger.LogNoneAsync($"ClientService.ListAsync: listing clients by filter ({filter}).");
            return await clientDataSource.ListAsync(filter);
        }

        public async Task<ClientModel> FindAsync(Guid id)
        {
            Guard.ValidateIdentifier(id, nameof(id));

            await logger.LogNoneAsync($"ClientService.FindAsync: trying to get client by id ({id}).");
            var client = await clientDataSource.FindAsync(id);
            if (client == null)
            {
                await logger.LogInformationAsync($"ClientService.FindAsync: client not found - {id}.");
                throw new ClientNotFoundException(id);
            }

            return client;
        }
    }
}
