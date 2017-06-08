using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllTemplate : IBllEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BllMaterial Material { get; set; }

        public BllWeldJoint WeldJoint { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string WeldingType { get; set; }

        public BllIndustrialObject IndustrialObject { get; set; }

        public BllCustomer Customer { get; set; }

        public string Contract { get; set; }

        public BllControlMethodsLib ControlMethodsLib { get; set; }
    }
}
