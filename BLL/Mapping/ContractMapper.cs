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
    public class ContractMapper : IContractMapper
    {

        public ContractMapper() { }
        public ContractMapper(IUnitOfWork uow)
        {

        }

        public DalContract MapToDal(BllContract entity)
        {
            return new DalContract
            {
                Id = entity.Id,
                Name = entity.Name,
                BeginDate = entity.BeginDate,
                EndDate = entity.EndDate,
                Lib_id = entity.ContractLib_id != null ? entity.ContractLib_id.Value : 0
            };
        }


        public BllContract MapToBll(DalContract entity)
        {
            BllContract bllEntity = new BllContract
            {
                Id = entity.Id,
                Name = entity.Name,
                BeginDate = entity.BeginDate,
                EndDate = entity.EndDate,
                ContractLib_id = entity.Lib_id
            };

            return bllEntity;
        }
    }
}
