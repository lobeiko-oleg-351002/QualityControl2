using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
    public class CustomerService : Service<BllCustomer, DalCustomer, Customer, CustomerMapper>, ICustomerService
    {
        //private readonly IUnitOfWork uow;

        public CustomerService(IUnitOfWork uow) : base(uow, uow.Customers)
        {
         //   this.uow = uow;
        }

        public override void Create(BllCustomer entity)
        {
            ContractLibService contractLibService = new ContractLibService(uow);
            var contractLib = contractLibService.Create(entity.ContractLib);
            entity.ContractLib = contractLib;
            uow.Customers.Create(mapper.MapToDal(entity));
            uow.Commit();
        }


        public override void Update(BllCustomer entity)
        {
            ContractLibService contractLibService = new ContractLibService(uow);
            contractLibService.Update(entity.ContractLib);
            uow.Customers.Update(mapper.MapToDal(entity));
            uow.Commit();
        }

        protected override void InitMapper()
        {
            mapper = new CustomerMapper(uow);
        }

        public BllCustomer GetCustomerByAddress(string address)
        {
            return mapper.MapToBll(uow.Customers.GetCustomerByAddress(address));
        }

        public BllCustomer GetCustomerByOrganization(string organization)
        {
            return mapper.MapToBll(uow.Customers.GetCustomerByOrganization(organization));
        }

        public BllCustomer GetCustomerByPhone(string phone)
        {
            return mapper.MapToBll(uow.Customers.GetCustomerByPhone(phone));
        }
    }
}
