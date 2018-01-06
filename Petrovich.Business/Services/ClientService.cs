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

        public async Task<ClientModelCollection> ListAsync(string filter, int pageIndex, int pageSize)
        {
            await logger.LogNoneAsync($"ClientService.ListAsync: listing clients by filter: {filter} pageIndex: {pageIndex} pageSize: {pageSize}.");
            return await clientDataSource.ListAsync(filter, pageIndex, pageSize);
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

        public async Task<ClientModel> CreateAsync(ClientModel client)
        {
            Guard.NotNullArgument(client, nameof(client));

            await logger.LogNoneAsync($"ClientService.CreateAsync trying to get the client with the same Passport Id: {client.PassportId}.");
            var samePassportClient = await clientDataSource.FindAsync(client.PassportId);
            if (samePassportClient != null)
            {
                await logger.LogInformationAsync($"ClientService.CreateAsync: client with the same Passport Id found: {samePassportClient.ClientId}.");
                throw new ClientPassportIdAlreadyExistException(samePassportClient.ClientId, samePassportClient.PassportId);
            }

            await logger.LogNoneAsync($"ClientService.CreateAsync creating new client.");
            return await clientDataSource.CreateAsync(client);
        }

        public async Task<ClientModel> UpdateAsync(ClientModel client)
        {
            Guard.ValidateIdentifier(client.ClientId, nameof(client.ClientId));

            await logger.LogNoneAsync($"ClientService.UpdateAsync: trying to get client by id ({client.ClientId}).");
            var dbClient = await clientDataSource.FindAsync(client.ClientId);
            if (dbClient == null)
            {
                await logger.LogInformationAsync($"ClientService.UpdateAsync: client not found - {client.ClientId}.");
                throw new ClientNotFoundException(client.ClientId);
            }
            
            await logger.LogNoneAsync($"ClientService.UpdateAsync: trying to get client with the same passport id ({client.PassportId}).");
            var passportIdClient = await clientDataSource.FindAsync(client.PassportId);
            if (passportIdClient != null && passportIdClient.ClientId != dbClient.ClientId)
            {
                await logger.LogInformationAsync($"ClientService.UpdateAsync: an other one client with the same password id found - {client.PassportId}.");
                throw new ClientPassportIdAlreadyExistException(passportIdClient.ClientId, passportIdClient.PassportId);
            }

            await logger.LogNoneAsync("ClientService.UpdateAsync: updating client.");
            return await clientDataSource.UpdateAsync(client);
        }
    }
}
