namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedEquipment")]
    public partial class SelectedEquipment : ISelectedEntity
    {
        public int id { get; set; }

        public int? entity_id { get; set; }

        public int? lib_id { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual EquipmentLib EquipmentLib { get; set; }
    }
}
