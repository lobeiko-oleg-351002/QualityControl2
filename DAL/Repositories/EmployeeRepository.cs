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
    public class EmployeeRepository : Repository<DalEmployee,Employee, EmployeeMapper>, IEmployeeRepository
    {
        private readonly ServiceDB context;
        public EmployeeRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public Employee GetOrmEmployeeByFio(string sirname, string name, string fathername)
        {
            return context.Set<Employee>().FirstOrDefault(e => (e.name == name) && (e.sirname == sirname) && (e.fathername == fathername));
        }

        public IEnumerable<DalEmployee> GetEmployeesByFatherName(string name)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.fathername == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
               // retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesByFunction(string function)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.function == function);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
               // retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesByName(string name)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.name == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
              //  retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesBySirname(string name)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.sirname == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
               // retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

     
    }
}
