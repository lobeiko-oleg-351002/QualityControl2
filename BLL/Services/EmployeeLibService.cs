using BLL.Entities;
using BLL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeLibService : EntityLibService<BllEmployee, EmployeeLib, BllEmployeeLib, SelectedEmployee, EntityLibMapper<BllEmployee, BllEmployeeLib, EmployeeService>, EmployeeService>
    {
        public EmployeeLibService(IUnitOfWork uow) : base(uow, uow.EmployeeLibs, uow.SelectedEmployees)
        {
            // this.uow = uow;
        }
    }
}
