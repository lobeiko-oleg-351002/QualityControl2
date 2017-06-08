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
    public class EquipmentRepository: Repository<DalEquipment,Equipment, EquipmentMapper>, IEquipmentRepository
    {
        private readonly ServiceDB context;
        public EquipmentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalEquipment> GetCheckedEquipment()
        {
            var elements = context.Equipments.Select(entity => entity.isChecked);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                //retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEquipment> GetEquipmentByFactoryNumber(string number)
        {
            var elements = context.Equipments.Select(entity => entity.factoryNumber == number);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                //retElemets.Add((element));
            }
            return retElemets;
        }

        public DalEquipment GetEquipmentByName(string name)
        {
            var ormEntity = context.Equipments.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        public IEnumerable<DalEquipment> GetEquipmentByType(string type)
        {
            var elements = context.Equipments.Select(entity => entity.type == type);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
               // retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEquipment> GetUncheckedEquipment()
        {
            var elements = context.Equipments.Select(entity => entity.isChecked);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
               // retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }


    }
}
