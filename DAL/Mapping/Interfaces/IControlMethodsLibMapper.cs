﻿using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IControlMethodsLibMapper : IMapper<DalControlMethodsLib, ControlMethodsLib>
    {
       // DalControlMethodsLib MapToDal(ControlMethodsLib entity);
       // ControlMethodsLib MapToOrm(DalControlMethodsLib entity);
    }
}
