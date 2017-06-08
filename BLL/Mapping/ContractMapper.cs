using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ContractMapper : IContractMapper
    {

        public DalContract MapToDal(BllContract entity)
        {
            return new DalContract
            {
                Id = entity.Id,
                Name = entity.Name,
                BeginDate = entity.BeginDate,
                EndDate = entity.EndDate,
                ContractLib_id = entity.ContractLib_id
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
                ContractLib_id = entity.ContractLib_id
            };

            return bllEntity;
        }
    }
}
