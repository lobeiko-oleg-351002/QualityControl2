using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEntitySimpleLibService<UEntity, TEntity> : IService<TEntity>
        where UEntity : IBllEntity
        where TEntity : class, IBllEntitySimpleLib<UEntity>
    {
        new TEntity Create(TEntity entity);
        new TEntity Update(TEntity entity);
    }
}
