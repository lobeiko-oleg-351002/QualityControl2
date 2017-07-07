using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllCertificateLib : IBllEntityLib<BllCertificate>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllCertificate>> SelectedEntities { get; set; }

        public BllCertificateLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllCertificate>>();
        }
    }
}
