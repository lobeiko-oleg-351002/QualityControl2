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
    public class ComponentLibService : EntityLibService<BllComponent, ComponentLib, BllComponentLib, SelectedComponent, EntityLibMapper<BllComponent, BllComponentLib, ComponentService>, ComponentService>
    {
        public ComponentLibService(IUnitOfWork uow) : base(uow, uow.ComponentLibs, uow.SelectedComponents)
        {
            // this.uow = uow;
        }
    }
}
