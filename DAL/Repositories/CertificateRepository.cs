using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CertificateRepository : Repository<DalCertificate, Certificate, CertificateMapper>, ICertificateRepository
    {
        private readonly ServiceDB context;
        public CertificateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }



        public DalCertificate GetCertificateByEmployeeIdAndControlId(int e_id, int c_id)
        {
            var ormEntity = context.Certificates.FirstOrDefault(entity => entity.employee_id == e_id && entity.controlName_id == c_id);
            return mapper.MapToDal(ormEntity);
        }

        public DalCertificate GetCertificateByTitle(string title)
        {
            var ormEntity = context.Certificates.FirstOrDefault(entity => entity.title == title);
            return mapper.MapToDal(ormEntity);
        }



    }
}
