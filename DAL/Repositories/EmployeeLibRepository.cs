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
    public class EmployeeLibRepository : Repository<DalEmployeeLib, EmployeeLib, EmployeeLibMapper>, IEmployeeLibRepository
    {
        private readonly ServiceDB context;
        public EmployeeLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


    }
}
