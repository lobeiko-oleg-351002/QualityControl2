using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : Repository<DalCustomer, Customer, CustomerMapper>, ICustomerRepository
    {
        private readonly ServiceDB context;
        public CustomerRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalCustomer GetCustomerByAddress(string address)
        {
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.address == address);
            return mapper.MapToDal(ormEntity);
        }

        public DalCustomer GetCustomerByOrganization(string organization)
        {
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.organization == organization);
            return mapper.MapToDal(ormEntity);
        }

        public DalCustomer GetCustomerByPhone(string phone)
        {
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.phone == phone);
            return mapper.MapToDal(ormEntity);
        }

    
    }
}
