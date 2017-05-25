using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Mappers
{
    public interface IBaseMapper<TBusinessEntity, TBusinessEntityCollection, TContextEntity> 
        where TBusinessEntity : class, new()
        where TBusinessEntityCollection : List<TBusinessEntity> ,new()
        where TContextEntity: class, new()
    {
        TBusinessEntity ToBusinessEntity(TContextEntity entity);
        TBusinessEntityCollection ToBusinessEntityCollection(IEnumerable<TContextEntity> entities);
        TContextEntity ToContextEntity(TBusinessEntity entity);
    }
}
