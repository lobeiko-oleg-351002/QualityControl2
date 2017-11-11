using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IJournalService : IService<BllJournal>, ILiteGetter<LiteJournal>
    {
        new BllJournal Create(BllJournal entity);
        new BllJournal Update(BllJournal entity);
        int GetCountOfRows();
        //List<BllJournal> GetAllWithoutControlMethodsLibs();
        LiteJournal GetLiteJournal(BllJournal entity);
        LiteJournal GetLiteJournal(int id);
    }
}
