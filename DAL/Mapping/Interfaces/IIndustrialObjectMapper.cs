using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IIndustrialObjectMapper : IMapper<DalIndustrialObject, IndustrialObject>
    {
       // DalIndustrialObject MapToDal(IndustrialObject entity);
        //IndustrialObject MapToOrm(DalIndustrialObject entity);
    }
}
