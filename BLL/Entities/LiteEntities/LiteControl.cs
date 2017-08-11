using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class LiteControl : IBllEntity
    {
        public int Id { get; set; }
        public bool? IsControlled { get; set; }
    }
}
