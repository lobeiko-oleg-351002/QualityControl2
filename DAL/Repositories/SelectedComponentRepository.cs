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
    public class SelectedComponentRepository : Repository<DalSelectedComponent, SelectedComponent, SelectedComponentMapper>, ISelectedComponentRepository
    {
        private readonly ServiceDB context;
        public SelectedComponentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedComponent> GetComponentsByLibId(int id)
        {
            var elements = context.Set<SelectedComponent>().Where(entity => entity.componentLib_id == id);
            var retElemets = new List<DalSelectedComponent>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }


    }
}
