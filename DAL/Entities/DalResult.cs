using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalResult : IDalEntityWithLibId
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Welder { get; set; }

        public string WeldingType { get; set; }

        public string DefectDescription { get; set; }

        public string Norm { get; set; }

        public string Quality { get; set; }

        public int Lib_id { get; set; }
    }
}
