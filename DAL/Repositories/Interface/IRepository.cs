using DAL.Entities.Interface;
using ORM;
using System.Collections.Generic;

namespace DAL.Repositories.Interface
{
    public interface IRepository<TEntity, UEntity> 
        where TEntity : IDalEntity 
        where UEntity : IOrmEntity
    {
        UEntity Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        void Delete(int id);

        void Update(TEntity entity);
    }
}
