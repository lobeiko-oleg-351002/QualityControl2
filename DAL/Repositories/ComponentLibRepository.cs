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
    public class ComponentLibRepository : Repository<DalComponentLib, ComponentLib, ComponentLibMapper>, IComponentLibRepository
    {
        private readonly ServiceDB context;
        public ComponentLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }
    }

}
