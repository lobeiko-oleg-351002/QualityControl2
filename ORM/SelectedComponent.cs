namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedComponent")]
    public partial class SelectedComponent : ISelectedEntity
    {
        public int id { get; set; }

        public int? entity_id { get; set; }

        public int? lib_id { get; set; }

        public virtual Component Component { get; set; }

        public virtual ComponentLib ComponentLib { get; set; }
    }
}
