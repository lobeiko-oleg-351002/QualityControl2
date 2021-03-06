namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedRequirementDocumentation")]
    public partial class SelectedRequirementDocumentation : ISelectedEntity
    {
        public int id { get; set; }

        public int entity_id { get; set; }

        public int lib_id { get; set; }

        public virtual RequirementDocumentation RequirementDocumentation { get; set; }

        public virtual RequirementDocumentationLib RequirementDocumentationLib { get; set; }
    }
}
