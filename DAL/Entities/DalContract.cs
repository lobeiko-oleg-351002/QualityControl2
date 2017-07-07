using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalContract : IDalEntityWithLibId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Lib_id { get; set; }
    }
}
