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
    public class ImageRepository : Repository<DalImage, Image, ImageMapper>, IImageRepository
    {
        private readonly ServiceDB context;
        public ImageRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalImage> GetImagesByLibId(int id)
        {
            ImageMapper mapper = new ImageMapper();
            var elements = context.Set<Image>().Where(entity => entity.imageLib_id == id);
            var retElemets = new List<DalImage>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

    

    }
}
