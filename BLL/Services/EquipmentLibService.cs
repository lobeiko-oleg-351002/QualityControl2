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
    public class EquipmentLibService : EntityLibService<BllEquipment, EquipmentLib, BllEquipmentLib, SelectedEquipment, EntityLibMapper<BllEquipment, BllEquipmentLib, EquipmentService>, EquipmentService>
    {
        public EquipmentLibService(IUnitOfWork uow) : base(uow, uow.EquipmentLibs, uow.SelectedEquipments)
        {
            // this.uow = uow;
        }
    }
}
