using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalImage : IDalEntityWithLibId
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int Lib_id { get; set; }
    }
}
