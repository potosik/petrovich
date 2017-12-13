using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Models
{
    public class ClientModel
    {
        public Guid ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public string PassportId { get; set; }
        public string PassportData { get; set; }
        public string PersonalId { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhonesJson { get; set; }
    }
}
