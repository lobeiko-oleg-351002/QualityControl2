using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalTemplate : IDalEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Material_id { get; set; }

        public int? Creator_id { get; set; }

        public int? WeldJoint_id { get; set; }

        public string Description { get; set; }

        public string Weight { get; set; }

        public string WeldingType { get; set; }

        public int? IndustrialObject_id { get; set; }
        
        public int? Customer_id { get; set; }

        public int? ControlMethodsLib_id { get; set; }

        public int? ScheduleOrganization_id { get; set; }

        public int? Contract_id { get; set; }
    }
}
