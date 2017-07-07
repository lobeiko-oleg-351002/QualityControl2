namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedControlName")]
    public partial class SelectedControlName : ISelectedEntity
    {
        public int id { get; set; }

        public int? entity_id { get; set; }

        public int? lib_id { get; set; }

        public virtual ControlName ControlName { get; set; }

        public virtual ControlNameLib ControlNameLib { get; set; }
    }
}
