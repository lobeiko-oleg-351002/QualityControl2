using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ContractService : Service<BllContract, DalContract>, IContractService
    {
        private readonly IUnitOfWork uow;

        public ContractService(IUnitOfWork uow) : base(uow, uow.Contracts)
        {
            this.uow = uow;
            bllMapper = new ContractMapper();
        }

        IContractMapper bllMapper;
        public override void Create(BllContract entity)
        {
            uow.Contracts.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllContract entity)
        {
            uow.Contracts.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllContract entity)
        {
            uow.Contracts.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllContract> GetAll()
        {
            var elements = uow.Contracts.GetAll();
            var retElemets = new List<BllContract>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllContract Get(int id)
        {
            return bllMapper.MapToBll(uow.Contracts.Get(id));
        }
    }
}
