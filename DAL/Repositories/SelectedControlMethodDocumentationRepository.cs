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
    public class SelectedControlMethodDocumentationRepository : Repository<DalSelectedControlMethodDocumentation, SelectedControlMethodDocumentation, SelectedControlMethodDocumentationMapper>, ISelectedControlMethodDocumentationRepository
    {
        private readonly ServiceDB context;
        public SelectedControlMethodDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedControlMethodDocumentation> GetControlMethodDocumentationsByLibId(int id)
        {
            var elements = context.Set<SelectedControlMethodDocumentation>().Where(entity => entity.controlMethodDocumentationLib_id == id);
            var retElemets = new List<DalSelectedControlMethodDocumentation>();
            foreach (var element in elements)
            {
                 retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }



    }
}
