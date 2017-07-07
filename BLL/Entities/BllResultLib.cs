using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllResultLib : IBllEntitySimpleLib<BllResult>
    {
        public int Id { get; set; }

        public List<BllResult> Entities { get; set; }

        public BllResultLib()
        {
            Entities = new List<BllResult>();
        }
    }
}
