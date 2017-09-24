using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class LiteJournal : IBllEntity
    {
        public int Id { get; set; }

        public DateTime? ControlDate { get; set; }

        public int? RequestNumber { get; set; }


        public int? Amount { get; set; }

        public string Weight { get; set; }

        public string MaterialName { get; set; }

        public string ScheduleOrganizationName { get; set; }

        public List<LiteControl> ControlMethods { get; set; }

        public string ComponentPressmark { get; set; }

        public string ComponentName { get; set; }

        public string Description { get; set; }

        public string IndustrialObjectName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ContractName { get; set; }
    }
}