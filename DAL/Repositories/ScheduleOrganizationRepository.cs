using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ScheduleOrganizationRepository : Repository<DalScheduleOrganization, ScheduleOrganization, ScheduleOrganizationMapper>, IScheduleOrganizationRepository
    {
        private readonly ServiceDB context;
        public ScheduleOrganizationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public ScheduleOrganization GetOrmScheduleOrganizationByName(string name)
        {
            return context.Set<ScheduleOrganization>().FirstOrDefault(e => e.name == name);
        }
    }
}
