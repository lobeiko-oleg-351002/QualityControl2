﻿using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface ICustomerMapper : IMapper<DalCustomer, Customer>
    {
        //DalCustomer MapToDal(Customer entity);
        //Customer MapToOrm(DalCustomer entity);
    }
}
