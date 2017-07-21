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
    public class ResultRepository : Repository<DalResult, Result, ResultMapper>, IResultRepository, IGetterByLibId<DalResult>
    {
        private readonly ServiceDB context;
        public ResultRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalResult GetResultByNumber(string number)
        {
            var ormEntity = context.Results.FirstOrDefault(entity => entity.number == number);
            return mapper.MapToDal(ormEntity);
        }

        public IEnumerable<DalResult> GetEntitiesByLibId(int id)
        {
            var elements = context.Set<Result>().Where(entity => entity.resultLib_id == id);
            var retElemets = new List<DalResult>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }



    }
}
