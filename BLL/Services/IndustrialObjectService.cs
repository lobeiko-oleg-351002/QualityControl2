﻿using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class IndustrialObjectService : Service<BllIndustrialObject, DalIndustrialObject>, IIndustrialObjectService
    {
        private readonly IUnitOfWork uow;
        IIndustrialObjectMapper bllMapper;
        public IndustrialObjectService(IUnitOfWork uow) : base(uow, uow.IndustrialObjects)
        {
            this.uow = uow;
            bllMapper = new IndustrialObjectMapper(uow);
        }

        public BllIndustrialObject GetIndustrialObjectByName(string name)
        {
            return bllMapper.MapToBll(uow.IndustrialObjects.GetIndustrialObjectByName(name));
        }

        public override void Create(BllIndustrialObject entity)
        {
            ComponentLibService ComponentLibService = new ComponentLibService(uow);
            var ComponentLib = ComponentLibService.Create(entity.ComponentLib);
            entity.ComponentLib = ComponentLib;
            uow.IndustrialObjects.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllIndustrialObject entity)
        {
            uow.IndustrialObjects.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllIndustrialObject entity)
        {
            ComponentLibService ComponentLibService = new ComponentLibService(uow);
            ComponentLibService.Update(entity.ComponentLib);
            uow.IndustrialObjects.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override BllIndustrialObject Get(int id)
        {
            DalIndustrialObject dalEntity = uow.IndustrialObjects.Get(id);
            return bllMapper.MapToBll(dalEntity);
        }

        public override IEnumerable<BllIndustrialObject> GetAll()
        {
            var elements = uow.IndustrialObjects.GetAll();
            var retElemets = new List<BllIndustrialObject>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        //private DalIndustrialObject MapBllToDal(BllIndustrialObject entity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<BllIndustrialObject, DalIndustrialObject>();
        //    });

        //    DalIndustrialObject dalEntity = Mapper.Map<DalIndustrialObject>(entity);
        //    dalEntity.ComponentLib_id = entity.ComponentLib != null ? entity.ComponentLib.Id : (int?)null;
        //    return dalEntity;
        //}

        //private BllIndustrialObject MapDalToBll(DalIndustrialObject entity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalIndustrialObject, BllIndustrialObject>();
        //    });
        //    BllIndustrialObject bllIndustrialObject = Mapper.Map<BllIndustrialObject>(entity);
        //    ComponentLibService ComponentLibService = new ComponentLibService(uow);
        //    bllIndustrialObject.ComponentLib = entity.ComponentLib_id != null ? ComponentLibService.Get((int)entity.ComponentLib_id) : null;
        //    return bllIndustrialObject;
        //}
    }
}
