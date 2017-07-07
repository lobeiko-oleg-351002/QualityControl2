using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllImageLib : IBllEntitySimpleLib<BllImage>
    {
        public int Id { get; set; }

        public List<BllImage> Entities { get; set; }

        public BllImageLib()
        {
            Entities = new List<BllImage>();
        }
    }
}
