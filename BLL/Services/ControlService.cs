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
    public class ControlService : Service<BllControl, DalControl, Control, ControlMapper>, IControlService
    {
       // private readonly IUnitOfWork uow;
        public ControlService(IUnitOfWork uow) : base(uow, uow.Controls)
        {
         //   this.uow = uow;
        }

        protected override void InitMapper()
        {
            mapper = new ControlMapper(uow);
        }

        public IEnumerable<BllControl> GetAllControlled()
        {
            var elements = uow.Controls.GetAllControlled();
            var retElemets = new List<BllControl>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllControl> GetAllUncontrolled()
        {
            var elements = uow.Controls.GetAllUncontrolled();
            var retElemets = new List<BllControl>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public BllControl GetControlByNumber(int number)
        {
            return mapper.MapToBll(uow.Controls.GetControlByNumber(number));
        }

        public new BllControl Create(BllControl entity)
        {
            ImageLibService imageLibService = new ImageLibService(uow);
            EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
            ControlMethodDocumentationLibService controlMethodDocumentationLibService = new ControlMethodDocumentationLibService(uow);
            RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            EmployeeLibService employeeLibService = new EmployeeLibService(uow);
            ResultLibService resultLibService = new ResultLibService(uow);

            var imageLib = imageLibService.Create(entity.ImageLib);
            entity.ImageLib = imageLib;
            entity.EquipmentLib = equipmentLibService.Create(entity.EquipmentLib);
            entity.ControlMethodDocumentationLib = controlMethodDocumentationLibService.Create(entity.ControlMethodDocumentationLib);
            entity.RequirementDocumentationLib = requirementDocumentationLibService.Create(entity.RequirementDocumentationLib);
            entity.EmployeeLib = employeeLibService.Create(entity.EmployeeLib);
            entity.ResultLib = resultLibService.Create(entity.ResultLib);
            
            return entity;

        }

        public override void Update(BllControl entity)
        {
            ResultLibService resultLibService = new ResultLibService(uow);
            resultLibService.Update(entity.ResultLib);
            EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
            equipmentLibService.Update(entity.EquipmentLib);
            uow.Controls.Update(mapper.MapToDal(entity));
            ControlMethodDocumentationLibService controlMethodDocumentationLibService = new ControlMethodDocumentationLibService(uow);
            controlMethodDocumentationLibService.Update(entity.ControlMethodDocumentationLib);
            RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            requirementDocumentationLibService.Update(entity.RequirementDocumentationLib);
            EmployeeLibService employeeLibService = new EmployeeLibService(uow);
            employeeLibService.Update(entity.EmployeeLib);
            uow.Commit();
        }
    }
}
