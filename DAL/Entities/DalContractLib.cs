﻿using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalContractLib : IDalEntity
    {
        public int Id { get; set; }
    }
}
