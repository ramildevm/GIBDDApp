namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DTPMembers
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FIO { get; set; }

        [Required]
        [StringLength(12)]
        public string DriverLicence { get; set; }

        [Required]
        [StringLength(9)]
        public string AvtoNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string RegNumber { get; set; }

        public string Photo { get; set; }

        public int DTPId { get; set; }

        public virtual DTP DTP { get; set; }
    }
}
