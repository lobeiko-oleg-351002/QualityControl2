using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService : Service<BllRole, DalRole, Role, RoleMapper>, IRoleService
    {
        //private readonly IUnitOfWork uow;

        public RoleService(IUnitOfWork uow) : base(uow, uow.Roles)
        {
         //   this.uow = uow;
        }


        public BllRole GetRoleByName(string name)
        {
            return mapper.MapToBll(uow.Roles.GetRoleByName(name));
        }
    }
}
