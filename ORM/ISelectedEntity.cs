using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public interface ISelectedEntity : IOrmEntity
    {
        int entity_id { get; set; }
        int lib_id { get; set; }
    }
}
