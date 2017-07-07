using BLL.Entities.Interface;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class EntitySimpleLibMapper<UEntity, YEntity, TEntity, Mapper> : IEntitySimpleLibMapper<UEntity, YEntity>
        where UEntity : IBllEntity
        where YEntity : IBllEntitySimpleLib<UEntity>
        where TEntity : IDalEntity
        where Mapper : class, IMapper<UEntity,TEntity>, new()
    {
        IGetterByLibId<TEntity> repository;
        IUnitOfWork uow;
        public EntitySimpleLibMapper() { }
        public EntitySimpleLibMapper(IGetterByLibId<TEntity> repository, IUnitOfWork uow)
        {
            this.repository = repository;
            this.uow = uow;
        }

        public IDalEntityLib MapToDal(YEntity entity)
        {
            return new DalEntityLib
            {
                Id = entity.Id
            };
        }

        public YEntity MapToBll(IDalEntityLib entity)
        {
            var bllEntity = (YEntity)Activator.CreateInstance(typeof(YEntity));
            bllEntity.Id = entity.Id;

            var mapper = (Mapper)Activator.CreateInstance(typeof(Mapper), uow);

            foreach (var item in repository.GetEntitiesByLibId(bllEntity.Id))
            {
                UEntity bllSelectedEntity = mapper.MapToBll(item);
                bllEntity.Entities.Add(bllSelectedEntity);
            }
            return bllEntity;
        }
    }
}