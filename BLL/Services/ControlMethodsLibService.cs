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
    public class ControlMethodsLibService : EntitySimpleLibService<BllControl, BllControlMethodsLib, DalControl, ControlMapper, ControlMethodsLib, Control>, IControlMethodsLibService
    {

        public ControlMethodsLibService(IUnitOfWork uow) : base(uow, uow.ControlMethodsLibs, uow.Controls)
        {

        }

        //ControlMethodsLibMapper bllMapper;

        public BllControlMethodsLib Create(BllControlMethodsLib entity, bool isTemplate)
        {
            var ormEntity = uow.ControlMethodsLibs.Create(mapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ControlService controlService = new ControlService(uow);
            IControlMapper controlMapper = new ControlMapper(uow);
            foreach (var Control in entity.Entities)
            {
                var control = controlService.Create(Control);
                var dalControl = controlMapper.MapToDal(Control);                
                dalControl.Lib_id = ormEntity.id;
                var ormControl = uow.Controls.Create(dalControl, isTemplate);
                uow.Commit();
                Control.Id = ormControl.id;
                Control.ProtocolNumber = ormControl.protocolNumber;
            }

            return entity;           
        }

        //public override BllControlMethodsLib Get(int id)
        //{
        //    var retElement = bllMapper.MapToBll(uow.ControlMethodsLibs.Get(id));

        //    return retElement;
        //}

        public new BllControlMethodsLib Update(BllControlMethodsLib entity, bool isTemplate)
        {
            IControlMapper controlMapper = new ControlMapper(uow);
            foreach (var Control in entity.Entities)
            {
                var currentControl = Control;
                if (Control.Id == 0)
                {
                    ControlService service = new ControlService(uow);
                    currentControl = service.Create(Control);
                    var dal = controlMapper.MapToDal(currentControl);
                    dal.Lib_id = entity.Id;
                    var ormControl = uow.Controls.Create(dal, isTemplate);
                    uow.Commit();
                    Control.Id = ormControl.id;
                    Control.ProtocolNumber = ormControl.protocolNumber;
                }
                else
                {
                    var dalControl = controlMapper.MapToDal(currentControl);
                    dalControl.Lib_id = entity.Id;

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

            var ControlsWithLibId = ((IGetterByLibId<DalControl>)entityRepository).GetEntitiesByLibId(entity.Id);
            foreach (var Control in ControlsWithLibId)
            {
                bool isTrashControl = true;
                foreach (var control in entity.Entities)
                {
                    if (control.Id == Control.Id)
                    {
                        isTrashControl = false;
                        break;
                    }
                }
                if (isTrashControl == true)
                {
                    uow.Controls.Delete(Control.Id);
                }
            }
            
            uow.Commit();

            return entity;
        }

    }
}
