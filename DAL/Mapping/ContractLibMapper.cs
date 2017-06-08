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
    public class ContractLibMapper : IContractLibMapper
    {
        public DalContractLib MapToDal(ContractLib entity)
        {
            return new DalContractLib
            {
                Id = entity.id,
            };
        }

        public ContractLib MapToOrm(DalContractLib entity)
        {
            return new ContractLib
            {
                id = entity.Id
            };
        }
    }
}
