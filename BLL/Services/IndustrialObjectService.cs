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
    public class IndustrialObjectService : Service<BllIndustrialObject, DalIndustrialObject, IndustrialObject, IndustrialObjectMapper>, IIndustrialObjectService
    {
       // private readonly IUnitOfWork uow;
        public IndustrialObjectService(IUnitOfWork uow) : base(uow, uow.IndustrialObjects)
        {
        //    this.uow = uow;
        }

        protected override void InitMapper()
        {
            mapper = new IndustrialObjectMapper(uow);
        }

        public BllIndustrialObject GetIndustrialObjectByName(string name)
        {
            return mapper.MapToBll(uow.IndustrialObjects.GetIndustrialObjectByName(name));
        }

        public override void Create(BllIndustrialObject entity)
        {
            //ComponentLibService ComponentLibService = new ComponentLibService(uow);
           // var ComponentLib = ComponentLibService.Create(entity.ComponentLib);
            //entity.ComponentLib = ComponentLib;
            uow.IndustrialObjects.Create(mapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllIndustrialObject entity)
        {
            //ComponentLibService ComponentLibService = new ComponentLibService(uow);
            //ComponentLibService.Update(entity.ComponentLib);
            uow.IndustrialObjects.Update(mapper.MapToDal(entity));
            uow.Commit();
        }

    }
}
