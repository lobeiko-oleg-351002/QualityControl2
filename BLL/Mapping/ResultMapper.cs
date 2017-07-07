using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ResultMapper : IResultMapper
    {
        public ResultMapper()
        {

        }

        public ResultMapper(IUnitOfWork uow) { }

        public DalResult MapToDal(BllResult entity)
        {
            DalResult dalEntity = new DalResult
            {
                Id = entity.Id,
                DefectDescription = entity.DefectDescription,
                WeldingType = entity.WeldingType,
                Norm = entity.Norm,
                Number = entity.Number,
                Quality = entity.Quality,
                Welder = entity.Welder,
            };

            return dalEntity;
        }

        public BllResult MapToBll(DalResult entity)
        {
            BllResult bllEntity = new BllResult
            {
                Id = entity.Id,
                DefectDescription = entity.DefectDescription,
                WeldingType = entity.WeldingType,
                Norm = entity.Norm,
                Number = entity.Number,
                Quality = entity.Quality,
                Welder = entity.Welder,
            };

            return bllEntity;
        }
    }
}
