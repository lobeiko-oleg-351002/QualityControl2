using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TemplateService : Service<BllTemplate, DalTemplate>, ITemplateService
    {
        private readonly IUnitOfWork uow;
        ITemplateMapper bllMapper;
        public TemplateService(IUnitOfWork uow) : base(uow, uow.Templates)
        {
            this.uow = uow;
            bllMapper = new TemplateMapper(uow);
        }

        public override void Create(BllTemplate entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            var controlMethodsLib = controlMethodsLibService.Create(entity.ControlMethodsLib);
            entity.ControlMethodsLib = controlMethodsLib;
            uow.Templates.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllTemplate entity)
        {
            uow.Templates.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllTemplate entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            controlMethodsLibService.Update(entity.ControlMethodsLib);
            uow.Templates.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override BllTemplate Get(int id)
        {
            DalTemplate dalEntity = uow.Templates.Get(id);
            return bllMapper.MapToBll(dalEntity);
        }

        public override IEnumerable<BllTemplate> GetAll()
        {
            var elements = uow.Templates.GetAll();
            var retElemets = new List<BllTemplate>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }
        public BllTemplate GetTemplateByName(string name)
        {
            DalTemplate dalEntity = uow.Templates.GetTemplateByName(name);
            return bllMapper.MapToBll(dalEntity);
        }
    }
}
