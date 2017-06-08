using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalCustomer : IDalEntity
    {
        public int Id { get; set; }

        public string Organization { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int? ContractLib_id { get; set; }

    }
}
