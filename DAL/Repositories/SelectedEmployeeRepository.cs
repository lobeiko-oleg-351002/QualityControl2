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
    public class SelectedEmployeeRepository : Repository<DalSelectedEmployee, SelectedEmployee, SelectedEmployeeMapper>, ISelectedEmployeeRepository
    {
        private readonly ServiceDB context;
        public SelectedEmployeeRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedEmployee> GetEmployeesByLibId(int id)
        {
            var elements = context.Set<SelectedEmployee>().Where(entity => entity.employeeLib_id == id);
            var retElemets = new List<DalSelectedEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }



    }
}
