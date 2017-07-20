using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TemplateService : Service<BllTemplate, DalTemplate, Template, TemplateMapper>, ITemplateService
    {
        public TemplateService(IUnitOfWork uow) : base(uow, uow.Templates)
        {
    
        }

        protected override void InitMapper()
        {
            mapper = new TemplateMapper(uow);
        }

        public override void Create(BllTemplate entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            var controlMethodsLib = controlMethodsLibService.Create(entity.ControlMethodsLib, true);
            entity.ControlMethodsLib = controlMethodsLib;
            uow.Templates.Create(mapper.MapToDal(entity));
            uow.Commit();
        }


        public override void Update(BllTemplate entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            controlMethodsLibService.Update(entity.ControlMethodsLib);
            uow.Templates.Update(mapper.MapToDal(entity));
            uow.Commit();
        }

        public BllTemplate GetTemplateByName(string name)
        {
            DalTemplate dalEntity = uow.Templates.GetTemplateByName(name);
            return mapper.MapToBll(dalEntity);
        }
    }
}
