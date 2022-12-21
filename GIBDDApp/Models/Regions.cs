namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Regions
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionNameEN { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionNameRU { get; set; }

        public int? SubjectCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string OkatoCode { get; set; }

        [StringLength(6)]
        public string ISO { get; set; }
    }
}
