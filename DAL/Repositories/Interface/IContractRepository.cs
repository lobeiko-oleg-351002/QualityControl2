using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IContractRepository : IRepository<DalContract>
    {
        IEnumerable<DalContract> GetContractsByLibId(int id);
        new Contract Create(DalContract entity);
        new void Delete(DalContract entity);
        new DalContract Get(int id);
        new IEnumerable<DalContract> GetAll();
        new void Update(DalContract entity);
    }
}
