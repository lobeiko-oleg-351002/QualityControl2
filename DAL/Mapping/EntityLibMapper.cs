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
    public class EntityLibMapper<UEntity> : IMapper<IDalEntityLib, UEntity>
        where UEntity : IOrmEntity
    {

        public IDalEntityLib MapToDal(UEntity entity)
        {
            return new DalEntityLib
            {
                Id = entity.id,
            };
        }

        public UEntity MapToOrm(IDalEntityLib entity)
        {
            var res = (UEntity)Activator.CreateInstance(typeof(UEntity));
            res.id = entity.Id;
            return res;
        }
    }
}
