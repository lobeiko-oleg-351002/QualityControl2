namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedEmployee")]
    public partial class SelectedEmployee : ISelectedEntity
    {
        public int id { get; set; }

        public int entity_id { get; set; }

        public int lib_id { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmployeeLib EmployeeLib { get; set; }
    }
}
