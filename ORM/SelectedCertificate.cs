namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedCertificate")]
    public partial class SelectedCertificate : ISelectedEntity
    {
        public int id { get; set; }

        public int entity_id { get; set; }

        public int lib_id { get; set; }

        public virtual Certificate Certificate { get; set; }

        public virtual CertificateLib CertificateLib { get; set; }
    }
}
