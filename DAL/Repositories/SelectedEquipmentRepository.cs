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
    public class SelectedEquipmentRepository : Repository<DalSelectedEquipment, SelectedEquipment, SelectedEquipmentMapper>, ISelectedEquipmentRepository
    {
        private readonly ServiceDB context;
        public SelectedEquipmentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        //public new SelectedEquipment Create(DalSelectedEquipment entity)
        //{
        //    var ormEntity = mapper.MapToOrm(entity);
        //    ormEntity.EquipmentLib = context.EquipmentLibs.FirstOrDefault(e => e.id == ormEntity.equipmentLib_id);
        //    //ormEntity.SelectedEquipmentLib.SelectedEquipment.Add(ormEntity);
        //    return context.Set<SelectedEquipment>().Add(ormEntity);
        //}

        public IEnumerable<DalSelectedEquipment> GetEquipmentsByLibId(int id)
        {
            var elements = context.Set<SelectedEquipment>().Where(entity => entity.equipmentLib_id == id);
            var retElemets = new List<DalSelectedEquipment>();
            if (elements != null)
            {
                foreach (var element in elements)
                {                   
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }



    }
}
