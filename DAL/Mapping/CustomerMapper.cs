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
    public class CustomerMapper : ICustomerMapper
    {
        public DalCustomer MapToDal(Customer entity)
        {
            return new DalCustomer
            {
                Id = entity.id,
                Address = entity.address,
                Organization = entity.organization,
                Phone = entity.phone,
                ContractLib_id = entity.contractLib_id,
            };
        }

        public Customer MapToOrm(DalCustomer entity)
        {
            return new Customer
            {
                id = entity.Id,
                address = entity.Address,
                contractLib_id = entity.ContractLib_id,
                organization = entity.Organization,
                phone = entity.Phone
            };
        }
    }
}
