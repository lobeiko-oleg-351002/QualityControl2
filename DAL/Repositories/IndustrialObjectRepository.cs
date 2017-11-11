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
    public class IndustrialObjectRepository: Repository<DalIndustrialObject, IndustrialObject, IndustrialObjectMapper>, IIndustrialObjectRepository
    {
        private readonly ServiceDB context;
        public IndustrialObjectRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalIndustrialObject GetIndustrialObjectByName(string name)
        {
            var ormEntity = context.IndustrialObjects.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        public IndustrialObject GetOrmIndustrialObjectByName(string name)
        {
            return context.Set<IndustrialObject>().FirstOrDefault(e => e.name == name);
        }
    }
}
