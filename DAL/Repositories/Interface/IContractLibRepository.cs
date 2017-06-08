using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IContractLibRepository : IRepository<DalContractLib>
    {
        new ContractLib Create(DalContractLib entity);
        new void Delete(DalContractLib entity);
        new DalContractLib Get(int id);
        new IEnumerable<DalContractLib> GetAll();
        new void Update(DalContractLib entity);
    }
}
