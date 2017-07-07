using BLL.Entities.Interface;
using DAL.Entities;
using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IEntitySimpleLibMapper<UEntity, TEntity> : IMapper<TEntity, IDalEntityLib>
        where UEntity : IBllEntity
        where TEntity : IBllEntitySimpleLib<UEntity>
    {
       // IDalEntityLib MapToDal(TEntity entity);
       // TEntity MapToBll(IDalEntityLib entity);
    }
}
