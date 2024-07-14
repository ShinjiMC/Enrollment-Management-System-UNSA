using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace course_microservice.test.repositories
{
    internal class AsyncHelper
    {
        internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _innerQueryProvider;

            internal TestAsyncQueryProvider(IQueryProvider inner)
            {
                _innerQueryProvider = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression) => _innerQueryProvider.Execute(expression);

            public TResult Execute<TResult>(Expression expression) => _innerQueryProvider.Execute<TResult>(expression);

            TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                Type expectedResultType = typeof(TResult).GetGenericArguments()[0];
                object? executionResult = ((IQueryProvider)this).Execute(expression);

                return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                    .MakeGenericMethod(expectedResultType)
                    .Invoke(null, new[] { executionResult });
            }
        }

        internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
                => new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _enumerator = inner;
            }

            public T Current => _enumerator.Current;

            public ValueTask DisposeAsync() => new(Task.Run(() => _enumerator.Dispose()));

            public ValueTask<bool> MoveNextAsync() => new(_enumerator.MoveNext());
        }
    }

    public static class MockDbSetHelper
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> data) where T : class
        {
            IQueryable<T> queryableData = data.AsQueryable();

            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new AsyncHelper.TestAsyncEnumerator<T>(queryableData.GetEnumerator()));

            dbSetMock.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new AsyncHelper.TestAsyncQueryProvider<T>(queryableData.Provider));

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression)
                .Returns(queryableData.Expression);

            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType)
                .Returns(queryableData.ElementType);

            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                .Returns(() => queryableData.GetEnumerator());

            return dbSetMock;
        }
    }
}