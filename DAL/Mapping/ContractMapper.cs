using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class ContractMapper : IContractMapper
    {
        public DalContract MapToDal(Contract entity)
        {
            return new DalContract
            {
                Id = entity.id,
                Name = entity.name,
                ContractLib_id = entity.contractLib_id,
                BeginDate = entity.beginDate,
                EndDate = entity.endDate                
            };
        }

        public Contract MapToOrm(DalContract entity)
        {
            return new Contract
            {
                id = entity.Id,
                name = entity.Name,
                contractLib_id = entity.ContractLib_id,
                beginDate = entity.BeginDate,
                endDate = entity.EndDate
            };
        }
    }
}
