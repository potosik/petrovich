using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.DatabaseProcessing
{
    public interface IDatabaseOperation<in TModel, TResult>
    {
        Task<TResult> RunAsync(TModel model);
    }
}
