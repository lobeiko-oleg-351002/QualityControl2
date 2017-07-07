using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Mapping;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CertificateLibService : EntityLibService<BllCertificate, CertificateLib, BllCertificateLib, SelectedCertificate, EntityLibMapper<BllCertificate, BllCertificateLib, CertificateService>, CertificateService >
    {
        public CertificateLibService(IUnitOfWork uow) : base(uow, uow.CertificateLibs, uow.SelectedCertificates )
        {
            // this.uow = uow;
        }
    }
}
