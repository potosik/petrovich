using Petrovich.Context;
using Petrovich.Context.DatabaseProcessing;
using Petrovich.Context.Entities;
using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.DataSource.Queries
{
    internal class ListClientsByFilterQuery : IDatabaseQuery<IPetrovichContext, IEnumerable<Client>>
    {
        private readonly string filter;
        private readonly int pageIndex;
        private readonly int pageSize;

        public ListClientsByFilterQuery(string filter, int pageIndex, int pageSize)
        {
            this.filter = filter;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        public async Task<IEnumerable<Client>> ExecuteAsync(IPetrovichContext model)
        {
            Guard.NotNullArgument(model, nameof(model));

            var query = model.Clients.AsQueryable();
            if (!String.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(item => item.PassportId.Contains(filter));
            }

            return await query
                .OrderBy(item => item.PassportId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
