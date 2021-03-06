﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities.Interface
{
    public interface IBllEntitySimpleLib<UEntity> : IBllEntity
        where UEntity : IBllEntity
    {
        List<UEntity> Entities { get; set; }
    }
}
