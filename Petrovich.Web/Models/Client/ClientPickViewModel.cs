using Petrovich.Business.Models;
using Petrovich.Core;
using Petrovich.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Client
{
    public class ClientPickViewModel
    {
        public ClientViewModel Client { get; set; }
        public string PickUri { get; set; }

        private ClientPickViewModel() { }

        public static ClientPickViewModel Create(ClientModel client, Uri returnUri, string parameter)
        {
            Guard.NotNullArgument(client, nameof(client));
            Guard.NotNullArgument(returnUri, nameof(returnUri));
            Guard.NotNullOrWhiteSpace(parameter, nameof(parameter));

            return new ClientPickViewModel()
            {
                Client = ClientViewModel.Create(client),
                PickUri = returnUri.SetParameter(parameter, client.ClientId.ToString()).ToString(),
            };
        }
    }
}