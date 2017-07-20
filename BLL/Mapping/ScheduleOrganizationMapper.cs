using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ScheduleOrganizationMapper : IScheduleOrganizationMapper
    {

        public ScheduleOrganizationMapper() { }
        public ScheduleOrganizationMapper(IUnitOfWork uow)
        {

        }

        public DalScheduleOrganization MapToDal(BllScheduleOrganization entity)
        {
            return new DalScheduleOrganization
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Description = entity.Description
            };
        }


        public BllScheduleOrganization MapToBll(DalScheduleOrganization entity)
        {
            BllScheduleOrganization bllEntity = new BllScheduleOrganization
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Description = entity.Description
            };

            return bllEntity;
        }
    }
}
