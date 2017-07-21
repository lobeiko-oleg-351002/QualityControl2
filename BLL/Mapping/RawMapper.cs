using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class RawMapper : IRawMapper
    {

        public RawMapper() { }
        public RawMapper(IUnitOfWork uow)
        {

        }

        public DalRaw MapToDal(BllRaw entity)
        {
            return new DalRaw
            {
                Id = entity.Id,
                Certificate = entity.Certificate,
                CertificateImage = entity.CertificateImage,
                DeliveryDate = entity.DeliveryDate,
                Documentation = entity.Documentation,
                IsValid = entity.IsValid,
                Name = entity.Name
            };
        }


        public BllRaw MapToBll(DalRaw entity)
        {
            BllRaw bllEntity = new BllRaw
            {
                Id = entity.Id,
                Certificate = entity.Certificate,
                CertificateImage = entity.CertificateImage,
                DeliveryDate = entity.DeliveryDate,
                Documentation = entity.Documentation,
                IsValid = entity.IsValid,
                Name = entity.Name
            };

            return bllEntity;
        }
    }
}
