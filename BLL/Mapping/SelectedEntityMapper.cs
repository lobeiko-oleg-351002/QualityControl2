using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class SelectedEntityMapper<UEntity, UService> : ISelectedEntityMapper<UEntity>
        where UEntity : IBllEntity
        where UService : IService<UEntity>
    {
        IUnitOfWork uow;
        public SelectedEntityMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IDalSelectedEntity MapToDal(IBllSelectedEntity<UEntity> entity)
        {
            DalSelectedEntity dalEntity = new DalSelectedEntity
            {
                Id = entity.Id,
                Entity_id = entity.Entity.Id,
            };

            return dalEntity;
        }

        public IBllSelectedEntity<UEntity> MapToBll(IDalSelectedEntity entity)
        {
            UService EntityService = (UService)Activator.CreateInstance(typeof(UService), uow);
            UEntity bllEntity = EntityService.Get((int)entity.Entity_id);

            BllSelectedEntity<UEntity> bllSelectedEntity = new BllSelectedEntity<UEntity>
            {
                Id = entity.Id,
                Entity = bllEntity
            };

            return bllSelectedEntity;
        }
    }
}
