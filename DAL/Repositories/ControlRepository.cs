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
    public class ControlRepository : Repository<DalControl, Control, ControlMapper>, IControlRepository, IGetterByLibId<DalControl>
    {
        private readonly ServiceDB context;
        public ControlRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


        public Control Create(DalControl entity, bool isTemplate)
        {
            var ormEntity = mapper.MapToOrm(entity);
            if (!isTemplate)
            {
                ormEntity.protocolNumber = GetLastProtocolNumberOfCurrentType(entity.ControlName_id.Value) + 1;
            }
            return context.Set<Control>().Add(ormEntity);
        }

        public IEnumerable<DalControl> GetAllControlled()
        {
            var elements = context.Controls.Select(entity => entity.isControlled == true);
            var retElemets = new List<DalControl>();

            return retElemets;
        }

        public IEnumerable<DalControl> GetAllUncontrolled()
        {
            var elements = context.Controls.Select(entity => entity.isControlled == false);
            var retElemets = new List<DalControl>();
            foreach (var element in elements)
            {
                //retElemets.Add(Mapper.Map<DalControl>(element));
            }
            return retElemets;
        }

        public DalControl GetControlByNumber(int number)
        {
            var ormEntity = context.Controls.FirstOrDefault(entity => entity.number == number);
            return mapper.MapToDal(ormEntity);
        }

        private int GetLastProtocolNumberOfCurrentType(int controlNameId)
        {
            var controls = context.Set<Control>().Where(entity => entity.controlName_id == controlNameId && entity.protocolNumber != null);
            return controls.Count() > 0 ? controls.AsEnumerable().Last().protocolNumber.Value : 0;
        }

        public IEnumerable<DalControl> GetEntitiesByLibId(int id)
        {
            var elements = context.Set<Control>().Where(entity => entity.controlMethodsLib_id == id);
            var retElemets = new List<DalControl>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

    
    }
}
