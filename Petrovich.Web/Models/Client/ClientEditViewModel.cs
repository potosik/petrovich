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
    public class ClientEditViewModel : ChangeTrackableViewModel
    {
        public ClientEditViewModel()
        {
        }

        public ClientEditViewModel(IChangeTrackableEntityModel entity)
            : base(entity)
        {
        }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        public Guid ClientId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Адрес проживания")]
        public string Address { get; set; }

        [Display(Name = "Адрес регистрации")]
        public string Registered { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "№ паспорта")]
        public string PassportId { get; set; }

        [Display(Name = "Паспорт выдан (кем, когда)")]
        public string PassportData { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Идентификационный №")]
        public string PersonalId { get; set; }

        [Required(ErrorMessageResourceName = "Required_Field_Error", ErrorMessageResourceType = typeof(Properties.Resources))]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Номера телефонов")]
        public string PhonesJson { get; set; }

        public static ClientEditViewModel Create(ClientModel client)
        {
            Guard.NotNullArgument(client, nameof(client));

            return new ClientEditViewModel(client)
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