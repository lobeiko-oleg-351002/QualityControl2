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
    public class ControlMethodDocumentationRepository : Repository<DalControlMethodDocumentation, ControlMethodDocumentation, ControlMethodDocumentationMapper>, IControlMethodDocumentationRepository
    {
        private readonly ServiceDB context;
        public ControlMethodDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalControlMethodDocumentation GetControlMethodDocumentationByName(string name)
        {
            var ormEntity = context.ControlMethodDocumentations.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }


    }
}
