using BLL.Entities.Interface;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllEmployeeLib : IBllEntityLib<BllEmployee>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllEmployee>> SelectedEntities { get; set; }

        public BllEmployeeLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllEmployee>>();
        }
    }
}

