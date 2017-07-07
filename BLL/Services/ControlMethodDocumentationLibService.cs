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
    public class ControlMethodDocumentationLibService : EntityLibService<BllControlMethodDocumentation, ControlMethodDocumentationLib, BllControlMethodDocumentationLib, SelectedControlMethodDocumentation, EntityLibMapper<BllControlMethodDocumentation, BllControlMethodDocumentationLib, ControlMethodDocumentationService>, ControlMethodDocumentationService>
    {
        public ControlMethodDocumentationLibService(IUnitOfWork uow) : base(uow, uow.ControlMethodDocumentationLibs, uow.SelectedControlMethodDocumentations)
        {
            // this.uow = uow;
        }
    }
}
