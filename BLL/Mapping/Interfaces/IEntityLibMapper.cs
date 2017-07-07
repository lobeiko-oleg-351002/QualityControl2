using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Interface;
using DAL.Entities;

namespace BLL.Mapping.Interfaces
{
    public interface IEntityLibMapper<UEntity, YEntity> : IMapper<YEntity, IDalEntityLib>
        where UEntity : IBllEntity
        where YEntity : IBllEntityLib<UEntity>

    {
       // IDalEntityLib MapToDal(YEntity entity);
       // YEntity MapToBll(IDalEntityLib entity);
    }
}
