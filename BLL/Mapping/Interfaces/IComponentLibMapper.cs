﻿using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IComponentLibMapper
    {
        DalComponentLib MapToDal(BllComponentLib entity);
        BllComponentLib MapToBll(DalComponentLib entity);
    }
}
