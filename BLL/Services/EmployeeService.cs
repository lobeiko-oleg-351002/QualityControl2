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
    public class EmployeeService : Service<BllEmployee, DalEmployee, Employee, EmployeeMapper>, IEmployeeService
    {
      //  private readonly IUnitOfWork uow;
        public EmployeeService(IUnitOfWork uow) : base(uow, uow.Employees)
        {
          //  this.uow = uow;
        }

        protected override void InitMapper()
        {
            mapper = new EmployeeMapper(uow);
        }

        public IEnumerable<BllEmployee> GetEmployeesByFatherName(string name)
        {
            var elements = uow.Employees.GetEmployeesByFatherName(name);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesByFunction(string function)
        {
            var elements = uow.Employees.GetEmployeesByFunction(function);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesByName(string name)
        {
            var elements = uow.Employees.GetEmployeesByName(name);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesBySirname(string name)
        {
            var elements = uow.Employees.GetEmployeesBySirname(name);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }
    }
}
