namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Journal = new HashSet<Journal>();
            Template = new HashSet<Template>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string organization { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public int? contractLib_id { get; set; }

        public virtual ContractLib ContractLib { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journal> Journal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Template> Template { get; set; }
    }
}
