using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalJournal : IDalEntity
    {
        public int Id { get; set; }

        public DateTime? ControlDate { get; set; }

        public int? RequestNumber { get; set; }

        public int? IndustrialObject_id { get; set; }

        public int? Amount { get; set; }

        public string Weight { get; set; }

        public int? Material_id { get; set; }

        public int? ScheduleOrganization_id { get; set; }

        public int? ControlMethodsLib_id { get; set; }

        public int? Component_id { get; set; }

        public int? Customer_id { get; set; }

        public string Description { get; set; }

        public string UserModifierLogin { get; set; }

        public int? UserOwner_id { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? Contract_id { get; set; }
    }
}
