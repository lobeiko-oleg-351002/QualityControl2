namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Template")]
    public partial class Template : IOrmEntity
    {
        public int id { get; set; }

        public int? requestNumber { get; set; }

        public int? industrialObject_id { get; set; }

        public int? amount { get; set; }

        [StringLength(50)]
        public string size { get; set; }

        public int? material_id { get; set; }

        public int? customer_id { get; set; }

        [StringLength(50)]
        public string weldingType { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public int? controlMethodsLib_id { get; set; }

        public int? scheduleOrganization_id { get; set; }

        public int? contract_id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? weldJoint_id { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual ControlMethodsLib ControlMethodsLib { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IndustrialObject IndustrialObject { get; set; }

        public virtual ScheduleOrganization ScheduleOrganization { get; set; }

        public virtual WeldJoint WeldJoint { get; set; }
    }
}
