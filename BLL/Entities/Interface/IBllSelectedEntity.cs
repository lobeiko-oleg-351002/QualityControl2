using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities.Interface
{
    public interface IBllSelectedEntity<UEntity> : IBllEntity
        where UEntity : IBllEntity
    {
        UEntity Entity { get; set; }
    }
}
