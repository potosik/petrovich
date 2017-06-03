using Moq;
using Petrovich.Context;
using Petrovich.Context.Entities.Base;
using Petrovich.Repositories.Tests.TestQueryProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Tests.Extensions
{
    public static class MockExtensions
    {
        public static Mock<IPetrovichContext> MockSet<T>(this Mock<IPetrovichContext> dbContext,
            IQueryable<T> data,
            Expression<Func<IPetrovichContext, IDbSet<T>>> expression) where T : BaseEntity, new()
        {
            var dbSet = new Mock<IDbSet<T>>();
            dbSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            
            dbContext.Setup(expression).Returns(dbSet.Object);

            return dbContext;
        }
    }
}
