using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context.DatabaseProcessing
{
    public interface IDatabaseOperation<in TModel>
    {
        Task RunAsync(TModel model);
    }
}
