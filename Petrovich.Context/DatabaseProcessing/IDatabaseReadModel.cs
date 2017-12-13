using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.DatabaseProcessing
{
    public interface IDatabaseReadModel<out TModel>
    {
        Task<TResult> QueryAsync<TResult>(Func<TModel, TResult> query);
        Task<TResult> QueryAsync<TResult>(IDatabaseQuery<TModel, TResult> query);

        Task DoAsync(IDatabaseOperation<TModel> command);
        Task<TResult> DoAsync<TResult>(IDatabaseOperation<TModel, TResult> command);
    }
}
