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
    public class ContractLibRepository : Repository<DalContractLib, ContractLib>, IContractLibRepository
    {
        private readonly ServiceDB context;
        public ContractLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        ContractLibMapper mapper = new ContractLibMapper();

        public new void Delete(DalContractLib entity)
        {
            var ormEntity = context.Set<ContractLib>().Single(ContractLib => ContractLib.id == entity.Id);
            context.Set<ContractLib>().Remove(ormEntity);
        }

        public new DalContractLib Get(int id)
        {
            var ormEntity = context.Set<ContractLib>().FirstOrDefault(ContractLib => ContractLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalContractLib> GetAll()
        {
            var elements = context.Set<ContractLib>().Select(ContractLib => ContractLib);
            var retElemets = new List<DalContractLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalContractLib entity)
        {
            var ormEntity = context.Set<ContractLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ContractLib Create(DalContractLib entity)
        {
            var res = context.Set<ContractLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
