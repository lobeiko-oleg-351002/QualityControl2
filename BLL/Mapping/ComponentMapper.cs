using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ComponentMapper : IComponentMapper
    {
        IUnitOfWork uow;
        public ComponentMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            templateService = new TemplateService(uow);
            industrialObjectService = new IndustrialObjectService(uow);
        }

        public ComponentMapper() { }

        public DalComponent MapToDal(BllComponent entity)
        {
            return new DalComponent
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                Template_id = entity.Template != null ? entity.Template.Id : (int?)null,
                Count = entity.Count,
                Description = entity.Description,
                IndustrialObject_id = entity.IndustrialObject != null ? entity.IndustrialObject.Id : (int?)null,
                
            };
        }

        ITemplateService templateService;
        IIndustrialObjectService industrialObjectService;

        public BllComponent MapToBll(DalComponent entity)
        {
            BllComponent bllEntity = new BllComponent
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                Template = entity.Template_id != null ? templateService.Get((int)entity.Template_id) : null,
                IndustrialObject = entity.IndustrialObject_id != null ? industrialObjectService.Get((int)entity.IndustrialObject_id) : null,
                Count = entity.Count,
                Description = entity.Description,

            };

            return bllEntity;
        }

        public LiteComponent MapDalToLiteBll(DalComponent entity)
        {
            var templateName = entity.Template_id != null ? uow.Templates.Get(entity.Template_id.Value).Name : "";
            LiteComponent bllEntity = new LiteComponent
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                TemplateName = entity.Template_id != null ? uow.Templates.Get(entity.Template_id.Value).Name : null,
                IndustrialObjectName = entity.IndustrialObject_id != null ? uow.IndustrialObjects.Get(entity.IndustrialObject_id.Value).Name : null,
                NameAndPressmarkAndTemplate = entity.Name + " " + entity.Pressmark + " " + templateName,
                Description = entity.Description,
                Count = entity.Count.Value
            };

            return bllEntity;
        }
    }
}
