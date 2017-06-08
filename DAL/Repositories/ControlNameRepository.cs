using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ControlNameRepository : Repository<DalControlName, ControlName, ControlNameMapper>, IControlNameRepository
    {
        private readonly ServiceDB context;
        public ControlNameRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalControlName GetControlNameByName(string name)
        {          
            var ormEntity = context.ControlNames.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
