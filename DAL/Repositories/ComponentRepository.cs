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

        public DalComponent GetComponentByName(string name)
        {
            var ormEntity = context.Components.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
