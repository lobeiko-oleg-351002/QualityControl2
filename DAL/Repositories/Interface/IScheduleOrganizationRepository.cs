﻿using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IScheduleOrganizationRepository : IRepository<DalScheduleOrganization, ScheduleOrganization>
    {
        ScheduleOrganization GetOrmScheduleOrganizationByName(string name);
    }
}
