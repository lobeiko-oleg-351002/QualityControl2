using DAL.Entities.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IMapperLight<TEntity, UEntity>
        where TEntity : IDalEntity
        where UEntity : IOrmEntity
    {
        TEntity MapToDalWithoutEntityFields(UEntity entity);
    }
}
