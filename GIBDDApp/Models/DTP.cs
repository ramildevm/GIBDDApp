namespace GIBDDApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DTP")]
    public partial class DTP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DTP()
        {
            DTPMembers = new HashSet<DTPMembers>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(500)]
        public string OtherInfo { get; set; }

        public int Victims { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public string Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTPMembers> DTPMembers { get; set; }
    }
}
