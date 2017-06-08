using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using ORM;
using System.Collections.Generic;
using System.Linq;
using System;
using DAL.Mapping.Interfaces;

namespace DAL.Repositories
{
    public class Repository<TEntity, UEntity, Mapper> : IRepository<TEntity, UEntity>
        where TEntity : class, IDalEntity
        where UEntity : class, IOrmEntity
        where Mapper : class, IMapper<TEntity, UEntity>, new()
    {
        private readonly ServiceDB context;
        protected Mapper mapper;

        public Repository(ServiceDB context)
        {
            this.context = context;
            mapper = new Mapper();
        }

        public UEntity Create(TEntity entity)
        {
            var res = context.Set<UEntity>().Add(mapper.MapToOrm(entity));
            return res;
        }

        public void Delete(TEntity entity)
        {
            var ormEntity = context.Set<UEntity>().Single(e => e.id == entity.Id);
            context.Set<UEntity>().Remove(ormEntity);
        }

        public TEntity Get(int id)
        {
            var ormEntity = context.Set<UEntity>().FirstOrDefault(e => e.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var elements = context.Set<UEntity>().Select(e => e);
            var retElemets = new List<TEntity>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public void Update(TEntity entity)
        {
            var ormEntity = context.Set<UEntity>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }
    }
}
