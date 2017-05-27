using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Models
{
    public class LogCollection : List<Log>
    {
        public LogCollection()
        {
        }

        public LogCollection(IEnumerable<Log> logs)
            : base(logs)
        {
        }
    }
}
