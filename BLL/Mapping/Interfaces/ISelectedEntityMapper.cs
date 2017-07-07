using BLL.Entities;
using BLL.Entities.Interface;
using DAL.Entities;
using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface ISelectedEntityMapper<UEntity> : IMapper<IBllSelectedEntity<UEntity>, IDalSelectedEntity>
        where UEntity : IBllEntity
    {
        new IBllSelectedEntity<UEntity> MapToBll(IDalSelectedEntity entity);
        new IDalSelectedEntity MapToDal(IBllSelectedEntity<UEntity> entity);
    }
}
