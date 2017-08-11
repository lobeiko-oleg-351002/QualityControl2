using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class LiteTemplate : IBllEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MaterialName { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string WeldingType { get; set; }

        public string IndustrialObjectName { get; set; }

        public string CustomerName { get; set; }

        public string ScheduleOrganizationName { get; set; }

        public List<string> ControlMethods { get; set; }
    }
}
