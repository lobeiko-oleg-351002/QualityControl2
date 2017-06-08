﻿using AutoMapper;
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
    public class ControlMethodsLibService : Service<BllControlMethodsLib, DalControlMethodsLib>, IControlMethodsLibService
    {
        private readonly IUnitOfWork uow;

        public ControlMethodsLibService(IUnitOfWork uow) : base(uow, uow.ControlMethodsLibs)
        {
            this.uow = uow;
            bllMapper = new ControlMethodsLibMapper(uow);
        }

        ControlMethodsLibMapper bllMapper;

        public new BllControlMethodsLib Create(BllControlMethodsLib entity)
        {
            var ormEntity = uow.ControlMethodsLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ControlService controlService = new ControlService(uow);
            IControlMapper controlMapper = new ControlMapper(uow);
            foreach (var Control in entity.Control)
            {
                var control = controlService.Create(Control);
                var dalControl = controlMapper.MapToDal(Control);                
                dalControl.ControlMethodsLib_id = ormEntity.id;
                var ormControl = uow.Controls.Create(dalControl);
                uow.Commit();
                Control.Id = ormControl.id;
                Control.ProtocolNumber = ormControl.protocolNumber;
               // bllEntity.Control.Add(ControlService.MapDalToBll(dalControl));
            }

            return entity;           
        }

        public override BllControlMethodsLib Get(int id)
        {
            var retElement = bllMapper.MapToBll(uow.ControlMethodsLibs.Get(id));

            return retElement;
        }

        public new BllControlMethodsLib Update(BllControlMethodsLib entity)
        {
            IControlMapper controlMapper = new ControlMapper(uow);
            foreach (var Control in entity.Control)
            {
                var currentControl = Control;
                if (Control.Id == 0)
                {
                    ControlService service = new ControlService(uow);
                    currentControl = service.Create(Control);
                    var dal = controlMapper.MapToDal(currentControl);
                    dal.ControlMethodsLib_id = entity.Id;
                    var ormControl = uow.Controls.Create(dal);
                    uow.Commit();
                    Control.Id = ormControl.id;
                    Control.ProtocolNumber = ormControl.protocolNumber;
                }
                else
                {
                    var dalControl = controlMapper.MapToDal(currentControl);
                    dalControl.ControlMethodsLib_id = entity.Id;

                    ImageLibService imageLibService = new ImageLibService(uow);
                    Control.ImageLib = imageLibService.Update(Control.ImageLib);
                    EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
                    Control.EquipmentLib = equipmentLibService.Update(Control.EquipmentLib);
                    ResultLibService resultLibService = new ResultLibService(uow);
                    Control.ResultLib = resultLibService.Update(Control.ResultLib);
                    RequirementDocumentationLibService reqDocLibService = new RequirementDocumentationLibService(uow);
                    Control.RequirementDocumentationLib = reqDocLibService.Update(Control.RequirementDocumentationLib);
                    ControlMethodDocumentationLibService methodDocLibService = new ControlMethodDocumentationLibService(uow);
                    Control.ControlMethodDocumentationLib = methodDocLibService.Update(Control.ControlMethodDocumentationLib);
                    EmployeeLibService employeeLibService = new EmployeeLibService(uow);
                    Control.EmployeeLib = employeeLibService.Update(Control.EmployeeLib);
                    uow.Controls.Update(dalControl);
                }
                
            }

            var ControlsWithLibId = uow.Controls.GetControlsByLibId(entity.Id);
            foreach (var Control in ControlsWithLibId)
            {
                bool isTrashControl = true;
                foreach (var control in entity.Control)
                {
                    if (control.Id == Control.Id)
                    {
                        isTrashControl = false;
                        break;
                    }
                }
                if (isTrashControl == true)
                {
                    uow.Controls.Delete(Control);
                }
            }
            
            uow.Commit();

            return entity;
        }

        //private BllControlMethodsLib MapDalToBll(DalControlMethodsLib dalEntity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalControlMethodsLib, BllControlMethodsLib>().ForMember(x => x.Control, opt => opt.Ignore());
        //    });
        //    BllControlMethodsLib bllEntity = Mapper.Map<BllControlMethodsLib>(dalEntity);
        //    ControlService controlService = new ControlService(uow);
        //    foreach (var Control in uow.Controls.GetControlsByLibId(dalEntity.Id))
        //    {
        //        bllEntity.Control.Add(controlService.Get(Control.Id));
        //    }
        //    return bllEntity;
        //}
    }
}
