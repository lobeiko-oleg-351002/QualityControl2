using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class TemplateMapper : ITemplateMapper
    {
        public DalTemplate MapToDal(Template entity)
        {
            return new DalTemplate
            {
                Id = entity.id,
                Description = entity.description,
                Material_id = entity.material_id,
                Name = entity.name,
                //WeldJoint_id = entity.weldJoint_id,
                ControlMethodsLib_id = entity.controlMethodsLib_id,
                Customer_id = entity.customer_id,
                IndustrialObject_id = entity.industrialObject_id,
                Weight = entity.weight,
                //WeldingType = entity.weldingType,
                Contract_id = entity.contract_id,
                ScheduleOrganization_id = entity.scheduleOrganization_id
            };
        }

        public Template MapToOrm(DalTemplate entity)
        {
            return new Template
            {
                id = entity.Id,
                description = entity.Description,
                material_id = entity.Material_id,
                name = entity.Name,
               // weldJoint_id = entity.WeldJoint_id,
                customer_id = entity.Customer_id,
                contract_id = entity.Contract_id,
                controlMethodsLib_id = entity.ControlMethodsLib_id,
                industrialObject_id = entity.IndustrialObject_id,
                weight = entity.Weight,
                scheduleOrganization_id = entity.ScheduleOrganization_id
                //weldingType = entity.WeldingType,
            };
        }
    }
}
