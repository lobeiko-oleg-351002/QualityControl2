using BLL.Entities;
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
    public class EntityLibMapper<UEntity, YEntity, Service> : IEntityLibMapper<UEntity, YEntity>
        where UEntity : class, IBllEntity
        where YEntity : class, IBllEntityLib<UEntity>
        where Service : class, IService<UEntity>
    {
        private readonly IUnitOfWork uow;
        private readonly IGetterByLibId<IDalSelectedEntity> repository;
        public EntityLibMapper() { }
        public EntityLibMapper(IUnitOfWork uow, IGetterByLibId<IDalSelectedEntity> repository)
        {
            this.uow = uow;
            this.repository = repository;
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

            SelectedEntityMapper<UEntity, Service> selectedEntityMapper = new SelectedEntityMapper<UEntity, Service>(uow);

            foreach (var item in repository.GetEntitiesByLibId(bllEntity.Id))
            {
                BllSelectedEntity<UEntity> bllSelectedEntity = (BllSelectedEntity < UEntity > )selectedEntityMapper.MapToBll((DalSelectedEntity)item);
                bllEntity.SelectedEntities.Add(bllSelectedEntity);
            }
            return bllEntity;
        }
    }
}
