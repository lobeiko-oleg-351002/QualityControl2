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
    public class SelectedCertificateRepository : Repository<DalSelectedCertificate, SelectedCertificate, SelectedCertificateMapper>, ISelectedCertificateRepository
    {
        private readonly ServiceDB context;
        public SelectedCertificateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedCertificate> GetCertificatesByLibId(int id)
        {
            var elements = context.Set<SelectedCertificate>().Where(entity => entity.certificateLib_id == id);
            var retElemets = new List<DalSelectedCertificate>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }


    }
}
