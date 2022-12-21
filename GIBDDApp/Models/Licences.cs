namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Licences
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Licences()
        {
            LicenseStatus = new HashSet<LicenseStatus>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DriverId { get; set; }

        [Column(TypeName = "date")]
        public DateTime LicenceDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpireDate { get; set; }

        [Required]
        [StringLength(25)]
        public string Categories { get; set; }

        [Required]
        [StringLength(5)]
        public string LicenceSeries { get; set; }

        public int LicenceNumber { get; set; }

        [Required]
        [StringLength(17)]
        public string VIN { get; set; }

        [Required]
        [StringLength(50)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        public int Year { get; set; }

        public int Weight { get; set; }

        public int Color { get; set; }

        public int EngineType { get; set; }

        [Required]
        [StringLength(50)]
        public string DriveType { get; set; }

        public virtual Colors Colors { get; set; }

        public virtual Drivers Drivers { get; set; }

        public virtual EngineTypes EngineTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LicenseStatus> LicenseStatus { get; set; }
    }
}
