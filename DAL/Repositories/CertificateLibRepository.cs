
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CertificateLibRepository : Repository<DalCertificateLib, CertificateLib, CertificateLibMapper>, ICertificateLibRepository
    {
        private readonly ServiceDB context;
        
        public CertificateLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


    }
}
