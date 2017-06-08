namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contract")]
    public partial class Contract : IOrmEntity
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? beginDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? endDate { get; set; }

        public int? contractLib_id { get; set; }

        public virtual ContractLib ContractLib { get; set; }
    }
}
