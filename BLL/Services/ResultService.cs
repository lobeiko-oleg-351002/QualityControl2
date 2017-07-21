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
    public class ResultService : Service<BllResult, DalResult, Result, ResultMapper>, IResultService
    {
       // private readonly IUnitOfWork uow;

        public ResultService(IUnitOfWork uow) : base(uow, uow.Results)
        {
         //   this.uow = uow;
        }

        public BllResult GetResultByNumber(string number)
        {
            return mapper.MapToBll(uow.Results.GetResultByNumber(number));
        }
    }
}
