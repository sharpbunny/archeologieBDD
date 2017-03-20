namespace tp10
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commune")]
    public partial class Commune
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commune()
        {
            site_intervention = new HashSet<site_intervention>();
        }

        [Key]
        public int ID_commune { get; set; }

        [Required]
        [StringLength(40)]
        public string nom { get; set; }

        public int? ID_departement { get; set; }

        public virtual departement departement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<site_intervention> site_intervention { get; set; }
    }
}
