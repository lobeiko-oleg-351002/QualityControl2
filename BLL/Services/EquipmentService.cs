using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EquipmentService :Service<BllEquipment, DalEquipment, Equipment, EquipmentMapper>, IEquipmentService
    {
      //  private readonly IUnitOfWork uow;
        public EquipmentService(IUnitOfWork uow) : base(uow, uow.Equipments)
        {
         //   this.uow = uow;
        }

        public IEnumerable<BllEquipment> GetCheckedEquipment()
        {
            var elements = uow.Equipments.GetCheckedEquipment();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEquipment> GetEquipmentByFactoryNumber(string number)
        {
            var elements = uow.Equipments.GetEquipmentByFactoryNumber(number);
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public BllEquipment GetEquipmentByName(string name)
        {
            return mapper.MapToBll(uow.Equipments.GetEquipmentByName(name));
        }

        public IEnumerable<BllEquipment> GetEquipmentByType(string type)
        {
            var elements = uow.Equipments.GetEquipmentByType(type);
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEquipment> GetUncheckedEquipment()
        {
            var elements = uow.Equipments.GetUncheckedEquipment();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToBll(element));
            }
            return retElemets;
        }
    }
}
