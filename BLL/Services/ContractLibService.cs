using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ContractLibService : Service<BllContractLib, DalContractLib, ContractLib>, IContractLibService
    {
        private readonly IUnitOfWork uow;
        IContractLibMapper bllMapper;
        public ContractLibService(IUnitOfWork uow) : base(uow, uow.ContractLibs)
        {
            this.uow = uow;
            bllMapper = new ContractLibMapper(uow);
        }

        public new BllContractLib Create(BllContractLib entity)
        {
            var ormEntity = uow.ContractLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ContractMapper ContractMapper = new ContractMapper();
            foreach (var Contract in entity.Contract)
            {
                var dalContract = ContractMapper.MapToDal(Contract);
                dalContract.ContractLib_id = entity.Id;
                var ormContract = uow.Contracts.Create(dalContract);
                uow.Commit();
                Contract.Id = ormContract.id;
            }
            return entity;
        }

        public override BllContractLib Get(int id)
        {
            var retElement = bllMapper.MapToBll(uow.ContractLibs.Get(id));
            var Contracts = uow.Contracts.GetContractsByLibId(retElement.Id);

            return retElement;
        }

        public new BllContractLib Update(BllContractLib entity)
        {
            ContractMapper ContractMapper = new ContractMapper();
            foreach (var Contract in entity.Contract)
            {
                if (Contract.Id == 0)
                {
                    var dalContract = ContractMapper.MapToDal(Contract);
                    dalContract.ContractLib_id = entity.Id;
                    var ormContract = uow.Contracts.Create(dalContract);
                    uow.Commit();
                    Contract.Id = ormContract.id;
                }

            }

            var ContractsWithLibId = uow.Contracts.GetContractsByLibId(entity.Id);
            foreach (var Contract in ContractsWithLibId)
            {
                bool isTrashContract = true;
                foreach (var contract in entity.Contract)
                {
                    if (contract.Id == Contract.Id)
                    {
                        isTrashContract = false;
                        break;
                    }
                }
                if (isTrashContract == true)
                {
                    uow.Contracts.Delete(Contract);
                }
            }
            uow.Commit();

            return entity;
        }
    }
}
