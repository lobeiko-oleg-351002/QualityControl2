using BLL.Entities.Interface;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EntitySimpleLibService<UEntity, TEntity, YEntity, EntityMapper, XEntity, ZEntity> : Service<TEntity, IDalEntityLib, XEntity, EntitySimpleLibMapper<UEntity, TEntity, YEntity, EntityMapper>>,  IEntitySimpleLibService<UEntity, TEntity>
        where UEntity : class, IBllEntity
        where TEntity : class, IBllEntitySimpleLib<UEntity>
        where YEntity : class, IDalEntityWithLibId
        where EntityMapper : class, IMapper<UEntity, YEntity>, new()
        where XEntity : class, IOrmEntity
        where ZEntity : class, IOrmEntity
    {
        protected readonly IRepository<YEntity, ZEntity> entityRepository; 
        public EntitySimpleLibService(IUnitOfWork uow, IRepository<IDalEntityLib, XEntity> libRepository, IRepository<YEntity, ZEntity> entityRepository) : base(uow, libRepository)
        {
            mapper = new EntitySimpleLibMapper<UEntity, TEntity, YEntity, EntityMapper>((IGetterByLibId<YEntity>)entityRepository, uow);
            this.entityRepository = entityRepository;
        }

        public new TEntity Create(TEntity entity)
        {
            var ormLib = repository.Create(mapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormLib.id;
            var EntityMapper = (EntityMapper)Activator.CreateInstance(typeof(EntityMapper), uow);
            foreach (var Entity in entity.Entities)
            {
                var dalEntity = EntityMapper.MapToDal(Entity);
                dalEntity.Lib_id = entity.Id;
                var ormEntity = entityRepository.Create(dalEntity);
                uow.Commit();
                Entity.Id = ormEntity.id;
            }
            return entity;
        }

        public override TEntity Get(int id)
        {
            var retElement = mapper.MapToBll(repository.Get(id));
            return retElement;
        }

        public new TEntity Update(TEntity entity)
        {
            EntityMapper EntityMapper = new EntityMapper();
            foreach (var Entity in entity.Entities)
            {
                if (Entity.Id == 0)
                {
                    var dalEntity = EntityMapper.MapToDal(Entity);
                    dalEntity.Lib_id = entity.Id;
                    var ormEntity = entityRepository.Create(dalEntity);
                    uow.Commit();
                    Entity.Id = ormEntity.id;
                }

            }

            var EntitysWithLibId = ((IGetterByLibId<YEntity>)entityRepository).GetEntitiesByLibId(entity.Id);
            foreach (var Entity in EntitysWithLibId)
            {
                bool isTrashEntity = true;
                foreach (var item in entity.Entities)
                {
                    if (item.Id == Entity.Id)
                    {
                        isTrashEntity = false;
                        break;
                    }
                }
                if (isTrashEntity == true)
                {
                    entityRepository.Delete(Entity.Id);
                }
            }
            uow.Commit();

            return entity;
        }
    }
}
