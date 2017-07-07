using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllSelectedEntity<UEntity> : IBllSelectedEntity<UEntity>
        where UEntity : IBllEntity
    {
        public UEntity Entity { get; set; }
        public int Id { get; set; }
    }
}
