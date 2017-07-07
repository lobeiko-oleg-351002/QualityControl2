using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IService<TEntity>
        where TEntity : IBllEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        void Create(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

    }
}
