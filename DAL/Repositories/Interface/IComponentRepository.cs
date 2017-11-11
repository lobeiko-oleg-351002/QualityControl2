using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IComponentRepository : IRepository<DalComponent, Component>
    {
        IEnumerable<DalComponent> GetComponentsByIndustrialObject(int id);
        int GetCountOfRows();
        List<DalComponent> GetAllLight();
        Component GetOrmComponentByNameAndPressmark(string name, string pressmark);
    }
}
