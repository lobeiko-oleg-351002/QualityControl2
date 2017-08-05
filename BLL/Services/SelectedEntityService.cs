using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SelectedEntityService<UEntity, TEntity, UService> : ISelectedEntityService<UEntity>
        where UEntity : IBllEntity
        where UService : IService<UEntity>
        where TEntity : class, ISelectedEntity
    {
        private readonly IUnitOfWork uow;
        ISelectedEntityMapper<UEntity> bllMapper;
        private readonly ISelectedEntityRepository<TEntity> repository;
        public SelectedEntityService(IUnitOfWork uow, ISelectedEntityRepository<TEntity> repository)
        {
            this.uow = uow;
            bllMapper = new SelectedEntityMapper<UEntity, UService>(uow);
            this.repository = repository;
        }

        public void Create(IBllSelectedEntity<UEntity> entity)
        {
            repository.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public void Update(IBllSelectedEntity<UEntity> entity)
        {
            repository.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            uow.Commit();
        }

        public IEnumerable<IBllSelectedEntity<UEntity>> GetAll()
        {
            var elements = repository.GetAll();
            var retElemets = new List<IBllSelectedEntity<UEntity>>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public IBllSelectedEntity<UEntity> Get(int id)
        {
            return bllMapper.MapToBll(repository.Get(id));
        }
    }
}
