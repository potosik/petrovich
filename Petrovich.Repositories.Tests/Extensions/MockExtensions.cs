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
using Petrovich.Repositories.Tests.DbSet;
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
            
            //var dbSet = new Mock<IDbSet<T>>();
            //dbSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));
            //dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
            //dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            //dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            var dbSet = CreateDbSet<T>(data.ToList());


            dbContext.Setup(expression).Returns(dbSet.Object);
            dbContext.Setup(context => context.Set<T>()).Returns(dbSet.Object);

            return dbContext;
        }
        
        public static Mock<DbSet<TEntity>> CreateDbSet<TEntity>(IList<TEntity> data, Func<object[], TEntity> find = null)
            where TEntity : class, new()
        {
            return CreateDbSet<DbSet<TEntity>, TEntity>(data, find);
        }

        public static Mock<TDbSet> CreateDbSet<TDbSet, TEntity>(IList<TEntity> data, Func<object[], TEntity> find = null)
            where TDbSet : class, IDbSet<TEntity>
            where TEntity : class, new()
        {
            var source = data.AsQueryable();
            var mock = new Mock<TDbSet> { CallBase = true };
            mock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(source.Expression);
            mock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(source.ElementType);
            mock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
            mock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<TEntity>(source.Provider));
            mock.As<IDbAsyncEnumerable<TEntity>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));
            mock.As<IDbSet<TEntity>>().Setup(m => m.Create()).Returns(new TEntity());
            mock.As<IDbSet<TEntity>>().Setup(m => m.Add(It.IsAny<TEntity>())).Returns<TEntity>(i => { data.Add(i); return i; });
            mock.As<IDbSet<TEntity>>().Setup(m => m.Remove(It.IsAny<TEntity>())).Returns<TEntity>(i => { data.Remove(i); return i; });

            if (find != null)
            {
                mock.As<IDbSet<TEntity>>().Setup(m => m.Find(It.IsAny<object[]>())).Returns(find);
            }

            return mock;
        }

        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
        {

            private readonly IQueryProvider _inner;

            internal TestDbAsyncQueryProvider(IQueryProvider inner) { _inner = inner; }
            public IQueryable CreateQuery(Expression expression) { return new TestDbAsyncEnumerable<TEntity>(expression); }
            public IQueryable<TElement> CreateQuery<TElement>(Expression expression) { return new TestDbAsyncEnumerable<TElement>(expression); }
            public object Execute(Expression expression) { return _inner.Execute(expression); }
            public TResult Execute<TResult>(Expression expression) { return _inner.Execute<TResult>(expression); }
            public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) { return Task.FromResult(Execute(expression)); }
            public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) { return Task.FromResult(Execute<TResult>(expression)); }
        }

        internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>
        {
            public TestDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
            public TestDbAsyncEnumerable(Expression expression) : base(expression) { }
            public IDbAsyncEnumerator<T> GetAsyncEnumerator() { return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator()); }
            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() { return GetAsyncEnumerator(); }
            public IQueryProvider Provider { get { return new TestDbAsyncQueryProvider<T>(this); } }
        }

        internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
        {

            private readonly IEnumerator<T> _inner;

            public TestDbAsyncEnumerator(IEnumerator<T> inner) { _inner = inner; }
            public void Dispose() { _inner.Dispose(); }
            public Task<bool> MoveNextAsync(CancellationToken cancellationToken) { return Task.FromResult(_inner.MoveNext()); }
            public T Current { get { return _inner.Current; } }
            object IDbAsyncEnumerator.Current { get { return Current; } }
        }
    }
}
