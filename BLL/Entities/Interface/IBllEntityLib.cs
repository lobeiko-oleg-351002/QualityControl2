using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities.Interface
{
    public interface IBllEntityLib<TEntity> : IBllEntity
        //where UEntity : IBllSelectedEntity<TEntity>
        where TEntity : IBllEntity
    {
        List<BllSelectedEntity<TEntity>> SelectedEntities { get; set; }
    }
}
