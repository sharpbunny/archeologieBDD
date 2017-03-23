namespace tp10
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class site_intervention
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public site_intervention()
        {
            interventions = new HashSet<intervention>();
            periodes = new HashSet<periode>();
            themes = new HashSet<theme>();
            type_intervention = new HashSet<type_intervention>();
        }

        [Key]
        [StringLength(100)]
        public string ID_site { get; set; }

        [Required]
        [StringLength(150)]
        public string nom_site { get; set; }

        public float latitude { get; set; }

        public float longitude { get; set; }

        public int ID_commune { get; set; }

        public virtual Commune Commune { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<intervention> interventions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<periode> periodes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<theme> themes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<type_intervention> type_intervention { get; set; }
    }
}
