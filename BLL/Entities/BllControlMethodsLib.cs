using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControlMethodsLib : IBllEntitySimpleLib<BllControl>
    {
        public int Id { get; set; }

        public List<BllControl> Entities { get; set; }

        public BllControlMethodsLib()
        {
            Entities = new List<BllControl>();
        }
    }
}
