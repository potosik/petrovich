using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class LogModelCollection : List<LogModel>
    {
        public int TotalCount { get; set; }

        public LogModelCollection()
        {
        }

        public LogModelCollection(IEnumerable<LogModel> logs)
            : base(logs)
        {
        }
    }
}
