using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ContractRepository : Repository<DalContract, Contract, ContractMapper>, IContractRepository, IGetterByLibId<DalContract>
    {
        private readonly ServiceDB context;
        public ContractRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalContract> GetEntitiesByLibId(int id)
        {
            ContractMapper mapper = new ContractMapper();
            var elements = context.Set<Contract>().Where(entity => entity.contractLib_id == id);
            var retElemets = new List<DalContract>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

    
    }
}
