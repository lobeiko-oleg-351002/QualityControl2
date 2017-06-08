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
    public class ControlMethodsLibRepository : Repository<DalControlMethodsLib, ControlMethodsLib, ControlMethodsLibMapper>, IControlMethodsLibRepository
    {
        private readonly ServiceDB context;
        public ControlMethodsLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


    }
}
