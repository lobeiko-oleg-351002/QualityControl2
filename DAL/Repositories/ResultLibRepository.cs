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
    public class ResultLibRepository : Repository<DalResultLib, ResultLib, ResultLibMapper>, IResultLibRepository
    {
        private readonly ServiceDB context;
        public ResultLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


    }
}