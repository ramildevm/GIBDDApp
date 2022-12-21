namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fines
    {
        public int Id { get; set; }

        [StringLength(9)]
        public string CarNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [Required]
        [StringLength(12)]
        public string Licence { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public string Photo { get; set; }
    }
}
