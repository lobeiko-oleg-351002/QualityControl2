using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IControlMethodDocumentationMapper : IMapper<DalControlMethodDocumentation, ControlMethodDocumentation>
    {
        //DalControlMethodDocumentation MapToDal(ControlMethodDocumentation entity);
        //ControlMethodDocumentation MapToOrm(DalControlMethodDocumentation entity);
    }
}
