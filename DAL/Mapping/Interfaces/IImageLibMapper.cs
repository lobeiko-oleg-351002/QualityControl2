﻿using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IImageLibMapper : IMapper<DalImageLib, ImageLib>
    {
        //DalImageLib MapToDal(ImageLib entity);
        //ImageLib MapToOrm(DalImageLib entity);
    }
}
