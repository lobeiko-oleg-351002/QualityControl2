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
    public class RequirementDocumentationRepository : Repository<DalRequirementDocumentation,RequirementDocumentation, RequirementDocumentationMapper>, IRequirementDocumentationRepository
    {
        private readonly ServiceDB context;
        public RequirementDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalRequirementDocumentation GetRequirementDocumentationByName(string name)
        {
            var ormEntity = context.RequirementDocumentations.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
