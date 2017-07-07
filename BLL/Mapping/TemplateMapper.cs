﻿using BLL.Entities;
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
    public class TemplateMapper : ITemplateMapper
    {
        IUnitOfWork uow;
        public TemplateMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            weldJointService = new WeldJointService(uow);
            materialService = new MaterialService(uow);
            industrialObjectService = new IndustrialObjectService(uow);
            customerService = new CustomerService(uow);
            controlMethodsLibService = new ControlMethodsLibService(uow);
        }

        public TemplateMapper() { }

        public DalTemplate MapToDal(BllTemplate entity)
        {
            DalTemplate dalEntity = new DalTemplate
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                WeldJoint_id = entity.WeldJoint != null ? entity.WeldJoint.Id : (int?)null,
                Material_id = entity.Material != null ? entity.Material.Id : (int?)null,
                Contract = entity.Contract,
                ControlMethodsLib_id = entity.ControlMethodsLib.Id,
                Customer_id = entity.Customer != null ? entity.Customer.Id : (int?)null,
                IndustrialObject_id = entity.IndustrialObject != null ? entity.IndustrialObject.Id : (int?)null,
                Size = entity.Size,
                WeldingType = entity.WeldingType
            };

            return dalEntity;
        }

        IWeldJointService weldJointService;
        IMaterialService materialService;
        IIndustrialObjectService industrialObjectService;
        ICustomerService customerService;
        IControlMethodsLibService controlMethodsLibService;

        public BllTemplate MapToBll(DalTemplate entity)
        {
            BllTemplate bllEntity = new BllTemplate
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                WeldJoint = entity.WeldJoint_id != null ? weldJointService.Get((int)entity.WeldJoint_id) : null,
                Material = entity.Material_id != null ? materialService.Get((int)entity.Material_id) : null,
                Customer = entity.Customer_id != null ? customerService.Get((int)entity.Customer_id) : null,
                Contract = entity.Contract,
                ControlMethodsLib = entity.ControlMethodsLib_id != null ? controlMethodsLibService.Get((int)entity.ControlMethodsLib_id) : null,
                IndustrialObject = entity.IndustrialObject_id != null ? industrialObjectService.Get((int)entity.IndustrialObject_id) : null,
                Size = entity.Size,
                WeldingType = entity.WeldingType
            };

            return bllEntity;
        }
    }
}
