using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Interface
{
    public interface IDalEntityWithLibId : IDalEntity
    {
        int Lib_id { get; set; }
    }
}
