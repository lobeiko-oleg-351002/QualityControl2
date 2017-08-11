using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface ILiteGetter<U>
        where U : IBllEntity
    {
        List<U> GetAllLite();
    }
}
