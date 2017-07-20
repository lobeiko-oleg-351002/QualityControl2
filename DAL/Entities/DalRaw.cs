using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalRaw : IDalEntity
    {
        public int Id { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Documentation { get; set; }

        public bool IsValid { get; set; }

        public string Certificate { get; set; }

        public byte[] CertificateImage { get; set; }

        public string Name { get; set; }
    }
}
