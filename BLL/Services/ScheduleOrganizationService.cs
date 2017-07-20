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
    public class ScheduleOrganizationService : Service<BllScheduleOrganization, DalScheduleOrganization, ScheduleOrganization, ScheduleOrganizationMapper>, IScheduleOrganizationService
    {

        public ScheduleOrganizationService(IUnitOfWork uow) : base(uow, uow.ScheduleOrganizations)
        {
        }

    }
}
