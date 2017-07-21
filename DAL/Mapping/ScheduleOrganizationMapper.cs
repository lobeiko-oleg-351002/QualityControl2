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
    public class ScheduleOrganizationMapper : IScheduleOrganizationMapper
    {
        public DalScheduleOrganization MapToDal(ScheduleOrganization entity)
        {
            return new DalScheduleOrganization
            {
                Id = entity.id,
                Address = entity.address,
                Description = entity.description,
                Name = entity.name
            };
        }

        public ScheduleOrganization MapToOrm(DalScheduleOrganization entity)
        {
            return new ScheduleOrganization
            {
                id = entity.Id,
                address = entity.Address,
                description = entity.Description,
                name = entity.Name
            };
        }
    }
}
