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
    public class ContractLibRepository : Repository<DalContractLib, ContractLib, ContractLibMapper>, IContractLibRepository
    {
        private readonly ServiceDB context;
        public ContractLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


    }
}
