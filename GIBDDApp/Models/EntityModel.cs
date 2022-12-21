using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GIBDDApp
{
    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<DTP> DTP { get; set; }
        public virtual DbSet<DTPMembers> DTPMembers { get; set; }
        public virtual DbSet<EngineTypes> EngineTypes { get; set; }
        public virtual DbSet<Fines> Fines { get; set; }
        public virtual DbSet<Licences> Licences { get; set; }
        public virtual DbSet<LicenseStatus> LicenseStatus { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colors>()
                .HasMany(e => e.Licences)
                .WithRequired(e => e.Colors)
                .HasForeignKey(e => e.Color);

            modelBuilder.Entity<Drivers>()
                .HasOptional(e => e.Licences)
                .WithRequired(e => e.Drivers)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DTP>()
                .HasMany(e => e.DTPMembers)
                .WithRequired(e => e.DTP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EngineTypes>()
                .HasMany(e => e.Licences)
                .WithRequired(e => e.EngineTypes)
                .HasForeignKey(e => e.EngineType);

            modelBuilder.Entity<Licences>()
                .HasMany(e => e.LicenseStatus)
                .WithRequired(e => e.Licences)
                .HasForeignKey(e => e.LicenceId);
        }
    }
}
