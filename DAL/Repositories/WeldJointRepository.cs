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
    public class WeldJointRepository : Repository<DalWeldJoint, WeldJoint, WeldJointMapper>, IWeldJointRepository
    {
        private readonly ServiceDB context;
        public WeldJointRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalWeldJoint GetWeldJointByName(string name)
        {
            var ormEntity = context.WeldJoints.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
