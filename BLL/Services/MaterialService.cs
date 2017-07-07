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
    public class MaterialService : Service<BllMaterial, DalMaterial, Material, MaterialMapper>, IMaterialService
    {
      //  private readonly IUnitOfWork uow;

        public MaterialService(IUnitOfWork uow) : base(uow, uow.Materials)
        {
           // this.uow = uow;
        }


        public BllMaterial GetMaterialByName(string name)
        {
            return mapper.MapToBll(uow.Materials.GetMaterialByName(name));
        }
    }
}
