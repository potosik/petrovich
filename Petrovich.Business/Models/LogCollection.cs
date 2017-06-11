using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class LogCollection : List<Log>
    {
        public int TotalCount { get; set; }

        public LogCollection()
        {
        }

        public LogCollection(IEnumerable<Log> logs)
            : base(logs)
        {
        }
    }
}
