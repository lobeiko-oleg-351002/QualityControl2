namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Journal")]
    public partial class Journal : IOrmEntity
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? controlDate { get; set; }

        public int? requestNumber { get; set; }

        public int? industrialObject_id { get; set; }

        public int? amount { get; set; }

        [StringLength(50)]
        public string weight { get; set; }

        public int? material_id { get; set; }

        public int? component_id { get; set; }

        public int? customer_id { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public int? controlMethodsLib_id { get; set; }

        public int? userOwner_id { get; set; }

        [StringLength(50)]
        public string userModifierLogin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modifiedDate { get; set; }

        public int? scheduleOrganization_id { get; set; }

        public int? contract_id { get; set; }

        public virtual Component Component { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ControlMethodsLib ControlMethodsLib { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IndustrialObject IndustrialObject { get; set; }

        public virtual Material Material { get; set; }

        public virtual ScheduleOrganization ScheduleOrganization { get; set; }

        public virtual User User { get; set; }
    }
}
