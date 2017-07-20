using DAL.Entities;
using DAL.Entities.Interface;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class SelectedEntityMapper<UEntity> : IMapper<IDalSelectedEntity, UEntity>
        where UEntity : class, ISelectedEntity, new()
    {
        public IDalSelectedEntity MapToDal(UEntity entity)
        {
            return new DalSelectedEntity
            {
                Id = entity.id,
                Lib_id = entity.lib_id,
                Entity_id = entity.entity_id
            };
        }

        public UEntity MapToOrm(IDalSelectedEntity entity)
        {
            return new UEntity
            {
                id = entity.Id,
                entity_id = entity.Entity_id,
                lib_id = entity.Lib_id
            };
        }
    }
}
