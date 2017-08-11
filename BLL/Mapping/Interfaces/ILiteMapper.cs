using BLL.Entities.Interface;
using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface ILiteMapper<T, U>
        where T: IBllEntity
        where U: IDalEntity
    {
        T MapDalToLiteBll(U entity);
    }
}
