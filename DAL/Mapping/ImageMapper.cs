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
    public class ImageMapper : IImageMapper
    {
        public DalImage MapToDal(Image entity)
        {
            return new DalImage
            {
                Id = entity.id,
                Image = entity.image,
                Lib_id = entity.imageLib_id.Value
            };
        }

        public Image MapToOrm(DalImage entity)
        {
            return new Image
            {
                id = entity.Id,
                image = entity.Image,
                imageLib_id = entity.Lib_id
            };
        }
    }
}
