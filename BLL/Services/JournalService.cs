using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JournalService : Service<BllJournal, DalJournal, Journal, JournalMapper>, IJournalService
    {

        public JournalService(IUnitOfWork uow) : base(uow, uow.Journals)
        {
        }

        protected override void InitMapper()
        {
            mapper = new JournalMapper(uow);
        }

        public new BllJournal Create(BllJournal entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            entity.ControlMethodsLib = controlMethodsLibService.Create(entity.ControlMethodsLib, false);
            DalJournal dalEntity = mapper.MapToDal(entity);
            Journal ormEntity = uow.Journals.Create(dalEntity);
            uow.Commit();
            entity.Id = ormEntity.id;
            return entity;
        }


        public new BllJournal Update(BllJournal entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            entity.ControlMethodsLib = controlMethodsLibService.Update(entity.ControlMethodsLib, false);
            uow.Journals.Update(mapper.MapToDal(entity));
            uow.Commit();

            return entity;
        }

    }
}
