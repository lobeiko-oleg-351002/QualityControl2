using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
    public class ImageService : Service<BllImage, DalImage, Image, ImageMapper>
    {
      //  private readonly IUnitOfWork uow;

        public ImageService(IUnitOfWork uow) : base(uow, uow.Images)
        {
      //      this.uow = uow;
        }
    }
}
