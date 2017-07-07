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
    public class RequirementDocumentationService : Service<BllRequirementDocumentation, DalRequirementDocumentation, RequirementDocumentation, RequirementDocumentationMapper>, IRequirementDocumentationService
    {
        //private readonly IUnitOfWork uow;

        public RequirementDocumentationService(IUnitOfWork uow) : base(uow, uow.RequirementDocumentations)
        {
          //  this.uow = uow;
        }


        public BllRequirementDocumentation GetRequirementDocumentationByName(string name)
        {
            return mapper.MapToBll(uow.RequirementDocumentations.GetRequirementDocumentationByName(name));
        }
    }
}
