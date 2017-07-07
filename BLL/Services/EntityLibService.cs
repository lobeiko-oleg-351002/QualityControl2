using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Mapping;
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

namespace BLL.Services
{
    public class EntityLibService<UEntity, YEntity, TEntity, XEntity, Mapper, Service> : Service<TEntity, IDalEntityLib, YEntity, Mapper>, IEntityLibService<UEntity, TEntity> 
        where UEntity : class, IBllEntity
        where YEntity : class, IOrmEntity
        where TEntity : class, IBllEntityLib<UEntity>
        where XEntity : class, ISelectedEntity
        where Mapper : class, IEntityLibMapper<UEntity, TEntity>, new()
        where Service : class, IService<UEntity>
    {
        //private readonly IUnitOfWork uow;
        //protected Mapper mapper;
        //private readonly IRepository<TEntity, YEntity> repository;
        //public EntityLibService(IUnitOfWork uow, IRepository<TEntity, YEntity> repository)
        //{
        //    this.uow = uow;
        //    mapper = new Mapper();
        //    this.repository = repository;
        //}
        EntityLibMapper<UEntity, TEntity, Service> libMapper;
        protected readonly IRepository<IDalSelectedEntity, XEntity> selectedEntityRepository;
        public EntityLibService(IUnitOfWork uow, IRepository<IDalEntityLib, YEntity> libRepository, IRepository<IDalSelectedEntity, XEntity> selectedEntityRepository) : base(uow, libRepository)
        {
            this.selectedEntityRepository = selectedEntityRepository;
            libMapper = new EntityLibMapper<UEntity, TEntity, Service>(uow, (IGetterByLibId<IDalSelectedEntity>)selectedEntityRepository);
        }

        protected override void InitMapper()
        {
            
        }

        public new TEntity Create(TEntity entity)
        {
            var ormLibEntity = repository.Create(libMapper.MapToDal(entity));
            var lib = entity;
            uow.Commit();
            entity.Id = ormLibEntity.id;
            ISelectedEntityMapper<UEntity> selectedEntityMapper = new SelectedEntityMapper<UEntity, Service>(uow);
            foreach (var Entity in lib.SelectedEntities)
            {
                var dalEntity = selectedEntityMapper.MapToDal(Entity);
                dalEntity.Lib_id = entity.Id;
                var ormEntity = selectedEntityRepository.Create(dalEntity);
                uow.Commit();
                Entity.Id = ormEntity.id;
            }

            return lib;
        }

        public new TEntity Get(int id)
        {
            return libMapper.MapToBll(repository.Get(id));
        }

        public new TEntity Update(TEntity entity)
        {
            ISelectedEntityMapper<UEntity> selectedEntityMapper = new SelectedEntityMapper<UEntity, Service>(uow);
            var lib = (IBllEntityLib<UEntity>)entity;
            foreach (var Entity in lib.SelectedEntities)
            {
                if (Entity.Id == 0)
                {
                    var dalEntity = selectedEntityMapper.MapToDal(Entity);
                    dalEntity.Lib_id = entity.Id;
                    selectedEntityRepository.Create(dalEntity);
                }
            }
            var EntitysWithLibId = ((IGetterByLibId<IDalSelectedEntity>)selectedEntityRepository).GetEntitiesByLibId(entity.Id);
            foreach (var Entity in EntitysWithLibId)
            {
                bool isTrashEntity = true;
                foreach (var selectedEntity in lib.SelectedEntities)
                {
                    if (Entity.Id == selectedEntity.Id)
                    {
                        isTrashEntity = false;
                        break;
                    }
                }
                if (isTrashEntity == true)
                {
                    selectedEntityRepository.Delete(Entity);
                }
            }
            uow.Commit();

            return (TEntity)lib;
        }

    }
}
