namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Raw")]
    public partial class Raw : IOrmEntity
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime deliveryDate { get; set; }

        [StringLength(50)]
        public string documentation { get; set; }

        public bool isValid { get; set; }

        [StringLength(50)]
        public string certificate { get; set; }

        [Column(TypeName = "image")]
        public byte[] certificateImage { get; set; }

        [StringLength(50)]
        public string name { get; set; }
    }
}
