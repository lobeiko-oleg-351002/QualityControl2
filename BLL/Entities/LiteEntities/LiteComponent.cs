﻿using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class LiteComponent : IBllEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TemplateName { get; set; }

        public string Pressmark { get; set; }

        public string IndustrialObjectName { get; set; }
    }
}
