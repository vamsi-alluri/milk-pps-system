using Microsoft.EntityFrameworkCore;
using pps_api.Entities;

namespace pps_api
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserIdentity> Identities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserDepartmentMapping> UserDepartments { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<VendorBankDetails> VendorBankDetails { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<VendorSubProduct> VendorSubProducts { get; set; }
        public DbSet<Entities.Route> Routes { get; set; }
        public DbSet<VendorRoute> VendorRoutes { get; set; }
        public DbSet<ProcessingPlant> ProcessingPlants { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserIdentity>()
                .HasMany(u => u.UserDepartmentMaps)
                .WithOne(ud => ud.UserIdentity)
                .HasForeignKey(ud => ud.UserIdentityID);

            // UserDepartment: composite key
            modelBuilder.Entity<UserDepartmentMapping>()
                .HasKey(ud => new { ud.UserIdentityID, ud.DepartmentID });

            modelBuilder.Entity<UserDepartmentMapping>()
                .HasOne(ud => ud.UserIdentity)
                .WithMany(u => u.UserDepartmentMaps)
                .HasForeignKey(ud => ud.UserIdentityID);

            modelBuilder.Entity<UserDepartmentMapping>()
                .HasOne(ud => ud.Department)
                .WithMany(d => d.UserDepartmentMaps)
                .HasForeignKey(ud => ud.DepartmentID);

            // Optional: enforce unique Role.Name and Department.Name if not handled by [Index]
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS"); // Case-insensitive collation

            // Vendor
            modelBuilder.Entity<Vendor>()
                .HasOne(v => v.Type)
                .WithMany()
                .HasForeignKey("VendorTypeId")
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Vendor>()
                .OwnsOne(v => v.BankDetails);

            // VendorRoute - Many-to-Many with temporal tracking
            modelBuilder.Entity<VendorRoute>()
                .HasKey(vr => new { vr.VendorId, vr.RouteId, vr.StartDate });

            modelBuilder.Entity<VendorRoute>()
                .HasOne(vr => vr.Vendor)
                .WithMany(v => v.VendorRoutes)
                .HasForeignKey(vr => vr.VendorId);

            modelBuilder.Entity<VendorRoute>()
                .HasOne(vr => vr.Route)
                .WithMany(r => r.VendorRoutes)
                .HasForeignKey(vr => vr.RouteId);

            modelBuilder.Entity<VendorRoute>()
                .HasIndex(vr => new { vr.VendorId })
                .HasFilter("[EndDate] IS NULL");

            // Route
            modelBuilder.Entity<Entities.Route>()
                .HasOne(r => r.AssociatedProcessingPlant)
                .WithMany()
                .HasForeignKey(r => r.ProcessingPlantId)
                .OnDelete(DeleteBehavior.Restrict);

            // SubProduct
            modelBuilder.Entity<SubProduct>()
                .HasOne(sp => sp.ProductType)
                .WithMany()
                .HasForeignKey(sp => sp.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // VendorSubProduct - Many-to-Many with cost
            modelBuilder.Entity<VendorSubProduct>()
                .HasKey(vsp => new { vsp.VendorId, vsp.SubProductId });

            modelBuilder.Entity<VendorSubProduct>()
                .HasOne(vsp => vsp.Vendor)
                .WithMany(v => v.VendorSubProducts)
                .HasForeignKey(vsp => vsp.VendorId);

            modelBuilder.Entity<VendorSubProduct>()
                .HasOne(vsp => vsp.SubProduct)
                .WithMany()
                .HasForeignKey(vsp => vsp.SubProductId);

            modelBuilder.Entity<ProcessingPlant>()
                .Property(p => p.Cost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SubProduct>()
                .HasOne(sp => sp.ProductUnit)
                .WithMany()
                .HasForeignKey(sp => sp.ProductUnitId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<VendorSubProduct>()
                .HasOne(vsp => vsp.ProductUnit)
                .WithMany()
                .HasForeignKey(vsp => vsp.ProductUnitId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
