using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IImageRepository : IRepository<DalImage, Image>
    {
        IEnumerable<DalImage> GetImagesByLibId(int id);

    }
}
