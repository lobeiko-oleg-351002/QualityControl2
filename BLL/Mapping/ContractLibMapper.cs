using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ContractLibMapper : IContractLibMapper
    {
        IUnitOfWork uow;
        public ContractLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalContractLib MapToDal(BllContractLib entity)
        {
            return new DalContractLib
            {
                Id = entity.Id
            };
        }

        public BllContractLib MapToBll(DalContractLib entity)
        {
            BllContractLib bllEntity = new BllContractLib
            {
                Id = entity.Id
            };

            IContractMapper ContractMapper = new ContractMapper();

            foreach (var Contract in uow.Contracts.GetContractsByLibId(bllEntity.Id))
            {
                var bllContract = ContractMapper.MapToBll(Contract);
                bllEntity.Contract.Add(bllContract);
            }
            return bllEntity;
        }
    }
}
