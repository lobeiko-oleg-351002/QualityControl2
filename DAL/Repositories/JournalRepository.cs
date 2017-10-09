using AutoMapper;
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
    public class JournalRepository : Repository<DalJournal, Journal, JournalMapper>, IJournalRepository
    {
        private readonly ServiceDB context;
        public JournalRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public int GetControlledCount(int componentId)
        {
            int res = 0;
            var ormJournals = context.Set<Journal>().Where(entity => entity.component_id == componentId);
            if (ormJournals.Any())
            {
                foreach (var item in ormJournals)
                {
                    res += item.amount.Value;
                }
            }
            return res;
        }

        public int GetCountOfRows()
        {
            return context.Journals.Count();
        }
    }
}
