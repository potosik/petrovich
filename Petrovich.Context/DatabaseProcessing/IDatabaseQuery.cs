using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.DatabaseProcessing
{
    public interface IDatabaseQuery<in TModel, TResult>
    {
        Task<TResult> ExecuteAsync(TModel model);
    }
}
