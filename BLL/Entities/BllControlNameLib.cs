using BLL.Entities.Interface;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControlNameLib : IBllEntityLib<BllControlName>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllControlName>> SelectedEntities { get; set; }

        public BllControlNameLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllControlName>>();
        }
    }
}
