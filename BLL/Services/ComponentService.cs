using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
    public class ComponentService : Service<BllComponent, DalComponent, Component, ComponentMapper>, IComponentService
    {
      //  private readonly IUnitOfWork uow;

        public ComponentService(IUnitOfWork uow) : base(uow, uow.Components)
        {
          //  this.uow = uow;
        }

        protected override void InitMapper()
        {
            mapper = new ComponentMapper(uow);
        }


        public List<BllComponent> GetComponentsByIndustrialObject(int id)
        {
            List<BllComponent> res = new List<BllComponent>();
            var components = uow.Components.GetComponentsByIndustrialObject(id);
            foreach(var item in components)
            {
                res.Add(mapper.MapToBll(item));
            }
            return res;
        }
    }

}
