using BLL.Entities;
using BLL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ControlNameLibService : EntityLibService<BllControlName, ControlNameLib, BllControlNameLib, SelectedControlName, EntityLibMapper<BllControlName, BllControlNameLib, ControlNameService>, ControlNameService>
    {
        public ControlNameLibService(IUnitOfWork uow) : base(uow, uow.ControlNameLibs, uow.SelectedControlNames)
        {
            // this.uow = uow;
        }
    }
}
