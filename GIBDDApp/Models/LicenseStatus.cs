namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LicenseStatus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int LicenceId { get; set; }

        public virtual Licences Licences { get; set; }
    }
}
