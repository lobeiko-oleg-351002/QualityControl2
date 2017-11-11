using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<DalEmployee, Employee>
    {
        IEnumerable<DalEmployee> GetEmployeesByName(string name);
        IEnumerable<DalEmployee> GetEmployeesByFatherName(string name);
        IEnumerable<DalEmployee> GetEmployeesBySirname(string name);
        IEnumerable<DalEmployee> GetEmployeesByFunction(string function);
        Employee GetOrmEmployeeByFio(string sirname, string name, string fathername);

    }
}
