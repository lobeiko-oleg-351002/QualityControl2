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
    public class ComponentRepository : Repository<DalComponent, Component, ComponentMapper>, IComponentRepository
    {
        private readonly ServiceDB context;
        public ComponentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalComponent> GetComponentsByIndustrialObject(int id)
        {
            var ormComponents = context.Set<Component>().Where(entity => entity.industrialObject_id == id);
            var res = new List<DalComponent>();
            if (ormComponents.Any())
            {
                foreach (var item in ormComponents)
                {
                    res.Add(mapper.MapToDal(item));
                }
            }
            return res;
        }

        public int GetCountOfRows()
        {
            return context.Components.Count();
        }
    }
}
