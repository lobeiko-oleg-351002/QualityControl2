using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
    public class ControlMethodDocumentationService : Service<BllControlMethodDocumentation, DalControlMethodDocumentation, ControlMethodDocumentation, ControlMethodDocumentationMapper>, IControlMethodDocumentationService
    {
       // private readonly IUnitOfWork uow;
        public ControlMethodDocumentationService(IUnitOfWork uow) : base(uow, uow.ControlMethodDocumentations)
        {
          //  this.uow = uow;

        }

        protected override void InitMapper()
        {
            mapper = new ControlMethodDocumentationMapper(uow);
        }

    }
}
