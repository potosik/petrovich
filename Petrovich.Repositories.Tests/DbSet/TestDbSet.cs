using Petrovich.Context.Entities.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Tests.DbSet
{
    public class TestDbSet<T> : DbSet<T> where T : BaseEntity, new()
    {
        private readonly IList<T> data;

        public TestDbSet(IEnumerable<T> data)
        {
            this.data = data.ToList();
        }

        public override ObservableCollection<T> Local => new ObservableCollection<T>(data);

        public override T Add(T entity)
        {
            data.Add(entity);
            return entity;
        }
    }
}
