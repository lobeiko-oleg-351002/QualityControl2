﻿using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalEquipment : IDalEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string FactoryNumber { get; set; }

        public DateTime? CheckDate { get; set; }

        public bool IsChecked { get; set; }

        public DateTime? TechnicalCheckDate { get; set; }

        public DateTime? NextTechnicalCheckDate { get; set; }

        public int? Сreator_id { get; set; }

        public string Pressmark { get; set; }

        public string NumberOfTechnicalCheck { get; set; }
    }
}
