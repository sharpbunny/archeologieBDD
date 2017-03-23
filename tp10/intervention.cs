namespace tp10
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("intervention")]
    public partial class intervention
    {
        [Key]
        public int id_intervention { get; set; }

        public DateTime? date_debut { get; set; }

        public DateTime? date_fin { get; set; }

        [Required]
        [StringLength(40)]
        public string ID_site { get; set; }

        public virtual site_intervention site_intervention { get; set; }
    }
}
