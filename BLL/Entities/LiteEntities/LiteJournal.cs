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

        public DateTime? RequestDate { get; set; }

        public int? RequestNumber { get; set; }


        public int? Amount { get; set; }

        public string Size { get; set; }

        public string WeldingType { get; set; }

        public string MaterialName { get; set; }

        public string ScheduleOrganizationName { get; set; }

        public List<LiteControl> ControlMethods { get; set; }

        public string ComponentName { get; set; }

        public string Description { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public string ContractName { get; set; }

        public string WeldJointName { get; set; }
    }
}
