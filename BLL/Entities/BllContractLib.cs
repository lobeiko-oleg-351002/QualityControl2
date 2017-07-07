using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllContractLib : IBllEntitySimpleLib<BllContract>
    {
        public int Id { get; set; }

        public List<BllContract> Entities { get; set; }

        public BllContractLib()
        {
            Entities = new List<BllContract>();
        }
    }
}
