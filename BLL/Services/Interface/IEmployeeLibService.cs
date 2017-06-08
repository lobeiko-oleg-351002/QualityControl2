﻿using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEmployeeLibService : IService<BllEmployeeLib>
    {
        new BllEmployeeLib Create(BllEmployeeLib entity);
        new BllEmployeeLib Update(BllEmployeeLib entity);
    }
}
