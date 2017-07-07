using BLL.Entities.Interface;
using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IMapper<TEntity, UEntity>
        where TEntity : IBllEntity
        where UEntity : IDalEntity
    {
        TEntity MapToBll(UEntity entity);
        UEntity MapToDal(TEntity entity);
    }
}
