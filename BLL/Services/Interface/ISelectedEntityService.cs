using BLL.Entities;
using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface ISelectedEntityService<UEntity> : IService<IBllSelectedEntity<UEntity>>
        where UEntity : IBllEntity
    {
        //IEnumerable<IBllSelectedEntity<UEntity>> GetAll();

        //IBllSelectedEntity<UEntity> Get(int id);

        //void Create(IBllSelectedEntity<UEntity> entity);

        //void Delete(IBllSelectedEntity<UEntity> entity);

        //void Update(IBllSelectedEntity<UEntity> entity);
    }
}
