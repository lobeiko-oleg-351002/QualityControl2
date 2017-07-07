using DAL.Entities;
using DAL.Entities.Interface;
using DAL.Mapping;
using DAL.Mapping.Interfaces;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EntityLibRepository<UEntity> : Repository<IDalEntityLib, UEntity, EntityLibMapper<UEntity>>
        where UEntity : class, IOrmEntity
    {
        private readonly ServiceDB context;

        public EntityLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }
    }
}
