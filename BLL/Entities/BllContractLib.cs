using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllContractLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllContract> Contract { get; set; }

        public BllContractLib()
        {
            Contract = new List<BllContract>();
        }
    }
}
