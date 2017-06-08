﻿using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllResult : IBllEntity
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public string Welder { get; set; }

        public string WeldingType { get; set; }

        public string DefectDescription { get; set; }

        public string Norm { get; set; }

        public string Quality { get; set; }
    }
}
