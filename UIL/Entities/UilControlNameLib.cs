﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilControlNameLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedControlName> SelectedControlName { get; set; }

        public UilControlNameLib()
        {
            SelectedControlName = new List<UilSelectedControlName>();
        }
    }
}
