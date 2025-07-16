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

            modelBuilder.Entity<Vendor>()
            .HasMany(v => v.SubProducts)
            .WithOne(sp => sp.Vendor)
            .HasForeignKey(sp => sp.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubProduct>()
                .HasOne(sp => sp.ProductType)
                .WithMany()
                .HasForeignKey(sp => sp.ProductTypeId);
        }

    }

}
