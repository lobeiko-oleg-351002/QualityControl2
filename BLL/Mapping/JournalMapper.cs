using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class JournalMapper : IJournalMapper
    {
        IUnitOfWork uow;

        IComponentService componentService;
        IControlMethodsLibService controlMethodsLibService;
        ICustomerService customerService;
        IIndustrialObjectService industrialObjectService;
        IMaterialService materialService;
        IUserService userService;
        IScheduleOrganizationService weldJointService;
        IContractService contractService;

        public JournalMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            componentService = new ComponentService(uow);
            controlMethodsLibService = new ControlMethodsLibService(uow);
            customerService = new CustomerService(uow);
            industrialObjectService = new IndustrialObjectService(uow);
            materialService = new MaterialService(uow);
            userService = new UserService(uow);
            weldJointService = new ScheduleOrganizationService(uow);
            contractService = new ContractService(uow);
        }

        public JournalMapper() { }

        public DalJournal MapToDal(BllJournal entity)
        {
            DalJournal dalEntity = new DalJournal
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Contract_id = entity.Contract != null ? entity.Contract.Id : (int?)null,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestNumber = entity.RequestNumber,
                Weight = entity.Weight,
                UserModifierLogin = entity.UserModifierLogin,
                Component_id = entity.Component != null ? entity.Component.Id : (int?)null,
                ControlMethodsLib_id = entity.ControlMethodsLib != null ? entity.ControlMethodsLib.Id : (int?)null,
                Customer_id = entity.Customer != null ? entity.Customer.Id : (int?)null,
                IndustrialObject_id = entity.IndustrialObject != null ? entity.IndustrialObject.Id : (int?)null,
                Material_id = entity.Material != null ? entity.Material.Id : (int?)null,
                UserOwner_id = entity.UserOwner != null ? entity.UserOwner.Id : (int?)null,
                ScheduleOrganization_id = entity.ScheduleOrganization != null ? entity.ScheduleOrganization.Id : (int?)null,
            };

            return dalEntity;
        }


        public BllJournal MapToBll(DalJournal entity)
        {
            BllJournal bllEntity = new BllJournal
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Contract = entity.Contract_id != null ? contractService.Get((int)entity.Contract_id) : null,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestNumber = entity.RequestNumber,
                Weight = entity.Weight,
                UserModifierLogin = entity.UserModifierLogin,
                Component = entity.Component_id != null ? componentService.Get((int)entity.Component_id) : null,
                ControlMethodsLib = entity.ControlMethodsLib_id != null ? controlMethodsLibService.Get((int)entity.ControlMethodsLib_id) : null,
                Customer = entity.Customer_id != null ? customerService.Get((int)entity.Customer_id) : null,
                IndustrialObject = entity.IndustrialObject_id != null ? industrialObjectService.Get((int)entity.IndustrialObject_id) : null,
                Material = entity.Material_id != null ? materialService.Get((int)entity.Material_id) : null,
                UserOwner = entity.UserOwner_id != null ? userService.Get((int)entity.UserOwner_id) : null,
                ScheduleOrganization = entity.ScheduleOrganization_id != null ? weldJointService.Get((int)entity.ScheduleOrganization_id) : null,
            };

            return bllEntity;
        }

        public BllJournal MapToBllWithoutControlMethodsLib(DalJournal entity)
        {
            BllJournal bllEntity = new BllJournal
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Contract = entity.Contract_id != null ? contractService.Get((int)entity.Contract_id) : null,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestNumber = entity.RequestNumber,
                Weight = entity.Weight,
                UserModifierLogin = entity.UserModifierLogin,
                Component = entity.Component_id != null ? componentService.Get((int)entity.Component_id) : null,
                ControlMethodsLib = new BllControlMethodsLib { Id = entity.ControlMethodsLib_id.Value },
                Customer = entity.Customer_id != null ? customerService.Get((int)entity.Customer_id) : null,
                IndustrialObject = entity.IndustrialObject_id != null ? industrialObjectService.Get((int)entity.IndustrialObject_id) : null,
                Material = entity.Material_id != null ? materialService.Get((int)entity.Material_id) : null,
                UserOwner = entity.UserOwner_id != null ? userService.Get((int)entity.UserOwner_id) : null,
                ScheduleOrganization = entity.ScheduleOrganization_id != null ? weldJointService.Get((int)entity.ScheduleOrganization_id) : null,
            };

            return bllEntity;
        }

        public LiteJournal MapDalToLiteBll(DalJournal entity)
        {
            List<LiteControl> controls = controlMethodsLibService.GetLiteControlsFromLib(entity.ControlMethodsLib_id.Value);
            DalComponent component = entity.Component_id != null ? uow.Components.Get(entity.Component_id.Value) : null;
            string industrialObject = "";
            if (component != null)
            {
                industrialObject = component.IndustrialObject_id != null ? uow.IndustrialObjects.Get(component.IndustrialObject_id.Value).Name : "";
            }

            LiteJournal bllEntity = new LiteJournal
            {
                Id = entity.Id,
                Amount = entity.Amount,
                ContractName = entity.Contract_id != null ? uow.Contracts.Get(entity.Contract_id.Value).Name : null,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestNumber = entity.RequestNumber,
                Weight = entity.Weight,
                ComponentPressmark = component != null ? component.Pressmark : null,
                ComponentName = component != null ? component.Name : null,
                IndustrialObjectName = industrialObject,
                MaterialName = entity.Material_id != null ? uow.Materials.Get(entity.Material_id.Value).Name : null,
                ScheduleOrganizationName = entity.ScheduleOrganization_id != null ? uow.ScheduleOrganizations.Get(entity.ScheduleOrganization_id.Value).Name : null,
                //WeldJointName = entity.WeldJoint_id != null ? uow.WeldJoints.Get(entity.WeldJoint_id.Value).Name : null,
                //WeldingType = entity.WeldingType,
                //RequestDate = entity.RequestDate,
                ControlMethods = controls
            };

            return bllEntity;
        }

        public LiteJournal MapBllToLiteBll(BllJournal entity)
        {
            List<LiteControl> controls = controlMethodsLibService.GetLiteControlsFromLib(entity.ControlMethodsLib.Id);
            LiteJournal bllEntity = new LiteJournal
            {
                Id = entity.Id,
                Amount = entity.Amount,
                ContractName = entity.Contract != null ? entity.Contract.Name : null,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestNumber = entity.RequestNumber,
                Weight = entity.Weight,
                ComponentPressmark = entity.Component != null ? entity.Component.Pressmark : null,
                MaterialName = entity.Material != null ? entity.Material.Name : null,
                ScheduleOrganizationName = entity.ScheduleOrganization != null ? entity.ScheduleOrganization.Name : null,
                //WeldJointName = entity.WeldJoint != null ? entity.WeldJoint.Name : null,
                //WeldingType = entity.WeldingType,
                //RequestDate = entity.RequestDate,
                ControlMethods = controls
            };

            return bllEntity;
        }
    }
}
