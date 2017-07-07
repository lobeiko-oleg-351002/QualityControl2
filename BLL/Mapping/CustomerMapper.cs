using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class CustomerMapper : ICustomerMapper
    {
        IUnitOfWork uow;
        public CustomerMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            contractLibService = new ContractLibService(uow);
        }

        public CustomerMapper() { }

        public DalCustomer MapToDal(BllCustomer entity)
        {
            DalCustomer dalEntity = new DalCustomer
            {
                Id = entity.Id,
                Address = entity.Address,
                ContractLib_id = entity.ContractLib.Id,
                Organization = entity.Organization,
                Phone = entity.Phone
            };

            return dalEntity;
        }

        IContractLibService contractLibService; 
        public BllCustomer MapToBll(DalCustomer entity)
        {
            BllCustomer bllEntity = new BllCustomer
            {
                Id = entity.Id,
                Address = entity.Address,
                ContractLib = entity.ContractLib_id != null ? contractLibService.Get((int)entity.ContractLib_id) : null,
                Organization = entity.Organization,
                Phone = entity.Phone
            };

            return bllEntity;
        }
    }
}
