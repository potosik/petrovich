using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Models
{
    public class ClientModelCollection : List<ClientModel>
    {
        public int TotalCount { get; set; }

        public ClientModelCollection()
        {
        }

        public ClientModelCollection(IEnumerable<ClientModel> clients)
            : base(clients)
        {
        }
    }
}
