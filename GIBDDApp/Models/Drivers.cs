namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Drivers
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(5)]
        public string PassportSeries { get; set; }

        public int PassportNumber { get; set; }

        public int PostCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string AddressLife { get; set; }

        [Required]
        [StringLength(50)]
        public string Company { get; set; }

        [Required]
        [StringLength(50)]
        public string Jobname { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Photo { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual Licences Licences { get; set; }
    }
}
