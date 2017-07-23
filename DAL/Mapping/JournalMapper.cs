using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class JournalMapper : IJournalMapper
    {
        public DalJournal MapToDal(Journal entity)
        {
            return new DalJournal
            {
                Id = entity.id,
                Component_id = entity.component_id,
                ControlDate = entity.controlDate,
                ControlMethodsLib_id = entity.controlMethodsLib_id,
                Customer_id = entity.customer_id,
                Amount = entity.amount,
                Description = entity.description,
                IndustrialObject_id = entity.industrialObject_id,
                Material_id = entity.material_id,
                ModifiedDate = entity.modifiedDate,
                RequestNumber = entity.requestNumber,
                Size = entity.size,
                UserModifierLogin = entity.userModifierLogin,
                UserOwner_id = entity.userOwner_id,
                ScheduleOrganization_id = entity.scheduleOrganization_id,
                Contract_id = entity.contract_id,
                RequestDate = entity.requestDate,
                WeldJoint_id = entity.weldJoint_id,
                WeldingType = entity.weldingType
            };
        }

        public Journal MapToOrm(DalJournal entity)
        {
            return new Journal
            {
                id = entity.Id,
                component_id = entity.Component_id,
                controlDate = entity.ControlDate,
                controlMethodsLib_id = entity.ControlMethodsLib_id,
                customer_id = entity.Customer_id,
                amount = entity.Amount,
                description = entity.Description,
                industrialObject_id = entity.IndustrialObject_id,
                material_id = entity.Material_id,
                modifiedDate = entity.ModifiedDate,
                requestNumber = entity.RequestNumber,
                size = entity.Size,
                userModifierLogin = entity.UserModifierLogin,
                userOwner_id = entity.UserOwner_id,
                scheduleOrganization_id = entity.ScheduleOrganization_id,
                contract_id = entity.Contract_id,
                weldJoint_id = entity.WeldJoint_id,
                requestDate = entity.RequestDate,
                weldingType = entity.WeldingType
            };
        }
    }
}
