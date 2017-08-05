using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IComponentService : IService<BllComponent>
    {
        List<BllComponent> GetComponentsByIndustrialObject(int id);
        // IEnumerable<BllComponent> GetComponentsByTemplateId(int id);
        int GetCountOfRows();
    }
}
