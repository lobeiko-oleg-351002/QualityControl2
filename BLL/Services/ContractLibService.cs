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
    public class ContractLibService : EntitySimpleLibService<BllContract, BllContractLib, DalContract, ContractMapper, ContractLib, Contract>, IContractLibService
    {
        public ContractLibService(IUnitOfWork uow) : base(uow, uow.ContractLibs, uow.Contracts)
        {
           // this.uow = uow;
        }

    }
}
