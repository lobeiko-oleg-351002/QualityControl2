using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Services.Interface;
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
    public class CertificateService : Service<BllCertificate, DalCertificate, Certificate, CertificateMapper>, ICertificateService
    {

        public CertificateService(IUnitOfWork uow) : base(uow, uow.Certificates)
        {
           // this.uow = uow;
        }

        protected override void InitMapper()
        {
            mapper = new CertificateMapper(uow);
        }

        public BllCertificate GetCertificateByTitle(string title)
        {
            return mapper.MapToBll(uow.Certificates.GetCertificateByTitle(title));
        }


        public BllCertificate GetCertificateByEmployeeAndControlName(BllEmployee employee, BllControlName name)
        {
            return mapper.MapToBll(uow.Certificates.GetCertificateByEmployeeIdAndControlId(employee.Id, name.Id));
        }

        public List<LiteCertificate> GetAllLite()
        {
            var elements = repository.GetAll();
            var retElemets = new List<LiteCertificate>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapDalToLiteBll(element));
            }
            return retElemets;
        }
    }
}
