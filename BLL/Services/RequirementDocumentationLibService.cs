using BLL.Entities;
using BLL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RequirementDocumentationLibService : EntityLibService<BllRequirementDocumentation, RequirementDocumentationLib, BllRequirementDocumentationLib, SelectedRequirementDocumentation, EntityLibMapper<BllRequirementDocumentation, BllRequirementDocumentationLib, RequirementDocumentationService>, RequirementDocumentationService>
    {
        public RequirementDocumentationLibService(IUnitOfWork uow) : base(uow, uow.RequirementDocumentationLibs, uow.SelectedRequirementDocumentations)
        {
            // this.uow = uow;
        }
    }
}
