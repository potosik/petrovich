using Petrovich.Business.Models;
using Petrovich.Business.Models.Base;
using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.Client
{
    public class ClientViewModel : ChangeTrackableViewModel
    {
        public ClientViewModel()
        {
        }

        public ClientViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
        }

        public Guid ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public string PassportId { get; set; }
        public string PassportData { get; set; }
        public string PersonalId { get; set; }

        [DisplayFormat(DataFormatString = "{0:" + Constants.Format.DateFormat + "}")]
        public DateTime BirthDate { get; set; }
        public string PhonesJson { get; set; }

        public static ClientViewModel Create(ClientModel client)
        {
            Guard.NotNullArgument(client, nameof(client));

            return new ClientViewModel(client)
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
            };
        }
    }
}