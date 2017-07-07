using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllComponentLib : IBllEntityLib<BllComponent>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllComponent>> SelectedEntities { get; set; }

        public BllComponentLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllComponent>>();
        }
    }
}
