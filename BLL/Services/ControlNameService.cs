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
    public class ControlNameService : Service<BllControlName, DalControlName, ControlName, ControlNameMapper>, IControlNameService
    {
        //private readonly IUnitOfWork uow;
        public ControlNameService(IUnitOfWork uow) : base(uow, uow.ControlNames)
        {
       //     this.uow = uow;
        }


    }
}
