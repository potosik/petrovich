using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Mappers
{
    public interface ILogMapper : IBaseMapper<Business.Models.Log, Business.Models.LogCollection, Log>
    {
    }
}
