using DAL.Entities;
using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IGetterByLibId<UEntity> 
        where UEntity : IDalEntity
    {
        IEnumerable<UEntity> GetEntitiesByLibId(int id);
    }
}
