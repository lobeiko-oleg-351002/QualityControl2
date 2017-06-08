using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Repositories.Interface
{
    public interface IRequirementDocumentationRepository : IRepository<DalRequirementDocumentation, RequirementDocumentation>
    {

        DalRequirementDocumentation GetRequirementDocumentationByName(string name);
    }
}
