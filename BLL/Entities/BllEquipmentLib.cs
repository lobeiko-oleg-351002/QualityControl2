using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllEquipmentLib : IBllEntityLib<BllEquipment>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllEquipment>> SelectedEntities { get; set; }

        public BllEquipmentLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllEquipment>>();
        }
    }
}
