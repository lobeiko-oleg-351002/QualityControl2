using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class RawMapper : IRawMapper
    {
        public DalRaw MapToDal(Raw entity)
        {
            return new DalRaw
            {
                Id = entity.id,
                Certificate = entity.certificate,
                DeliveryDate = entity.deliveryDate,
                Documentation = entity.documentation,
                CertificateImage = entity.certificateImage,
                IsValid = entity.isValid,
                Name = entity.name
            };
        }

        public Raw MapToOrm(DalRaw entity)
        {
            return new Raw
            {
                id = entity.Id,
                certificate = entity.Certificate,
                deliveryDate = entity.DeliveryDate,
                documentation = entity.Documentation,
                certificateImage = entity.CertificateImage,
                isValid = entity.IsValid,
                name = entity.Name
            };
        }
    }
}
