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
    public class ImageLibService : EntitySimpleLibService<BllImage, BllImageLib, DalImage, ImageMapper, ImageLib, Image>, IImageLibService
    {
        public ImageLibService(IUnitOfWork uow) : base(uow, uow.ImageLibs, uow.Images)
        {
           // this.uow = uow;
        }
    }
}
