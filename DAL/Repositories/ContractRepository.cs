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
    public class ContractRepository : Repository<DalContract, Contract>, IContractRepository
    {
        private readonly ServiceDB context;
        public ContractRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        ContractMapper mapper = new ContractMapper();

        public new void Delete(DalContract entity)
        {
            var ormEntity = context.Set<Contract>().Single(Contract => Contract.id == entity.Id);
            context.Set<Contract>().Remove(ormEntity);
        }

        public IEnumerable<DalContract> GetContractsByLibId(int id)
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

        public new DalContract Get(int id)
        {
            var ormEntity = context.Set<Contract>().FirstOrDefault(Contract => Contract.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalContract> GetAll()
        {
            var elements = context.Set<Contract>().Select(Contract => Contract);
            var retElemets = new List<DalContract>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalContract entity)
        {
            var ormEntity = context.Set<Contract>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Contract Create(DalContract entity)
        {
            var res = context.Set<Contract>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
