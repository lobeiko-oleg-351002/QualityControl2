﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Interface
{
    public interface IDalSelectedEntity : IDalEntityWithLibId
    {
        int Entity_id { get; set; }
    }
}
