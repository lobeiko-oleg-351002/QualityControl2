using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using AutoMapper;
using DAL.Mapping;

namespace DAL.Repositories
{
    public class RoleRepository : Repository<DalRole, Role, RoleMapper>, IRoleRepository
    {
        private readonly ServiceDB context;
        public RoleRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalRole GetRoleByName(string name)
        {
           
            var ormEntity = context.Roles.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
