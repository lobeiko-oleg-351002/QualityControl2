using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IEquipmentRepository : IRepository<DalEquipment, Equipment>
    {
        DalEquipment GetEquipmentByName(string name);
        IEnumerable<DalEquipment> GetEquipmentByType(string type);
        IEnumerable<DalEquipment> GetEquipmentByFactoryNumber(string number);
        IEnumerable<DalEquipment> GetCheckedEquipment();
        IEnumerable<DalEquipment> GetUncheckedEquipment();
    }
}
