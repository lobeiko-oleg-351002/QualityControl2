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
    public class SelectedRequirementDocumentationRepository : Repository<DalSelectedRequirementDocumentation, SelectedRequirementDocumentation, SelectedRequirementDocumentationMapper>, ISelectedRequirementDocumentationRepository
    {
        private readonly ServiceDB context;
        public SelectedRequirementDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedRequirementDocumentation> GetRequirementDocumentationsByLibId(int id)
        {
            var elements = context.Set<SelectedRequirementDocumentation>().Where(entity => entity.requirementDocumentationLib_id == id);
            var retElemets = new List<DalSelectedRequirementDocumentation>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }



    }
}
