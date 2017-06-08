using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IResultLibMapper : IMapper<DalResultLib, ResultLib>
    {
        //DalResultLib MapToDal(ResultLib entity);
        //ResultLib MapToOrm(DalResultLib entity);
    }
}
