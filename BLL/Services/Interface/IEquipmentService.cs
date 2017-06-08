using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEquipmentService : IService<BllEquipment>
    {
        BllEquipment GetEquipmentByName(string name);
        IEnumerable<BllEquipment> GetEquipmentByType(string type);
        IEnumerable<BllEquipment> GetEquipmentByFactoryNumber(string number);
        IEnumerable<BllEquipment> GetCheckedEquipment();
        IEnumerable<BllEquipment> GetUncheckedEquipment();
    }
}
