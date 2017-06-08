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
    public class TemplateRepository : Repository<DalTemplate, Template, TemplateMapper>, ITemplateRepository
    {
        private readonly ServiceDB context;
        public TemplateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalTemplate GetTemplateByName(string name)
        {
            var ormEntity = context.Templates.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
