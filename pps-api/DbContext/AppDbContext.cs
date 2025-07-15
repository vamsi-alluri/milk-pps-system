using Microsoft.EntityFrameworkCore;
using pps_api.Entities;

namespace pps_api
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserIdentity> Identities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserDepartment: composite key
            modelBuilder.Entity<UserDepartment>()
                .HasKey(ud => new { ud.UserIdentityID, ud.DepartmentID });

            modelBuilder.Entity<UserDepartment>()
                .HasOne(ud => ud.UserIdentity)
                .WithMany(u => u.UserDepartments)
                .HasForeignKey(ud => ud.UserIdentityID);

            modelBuilder.Entity<UserDepartment>()
                .HasOne(ud => ud.Department)
                .WithMany(d => d.UserDepartments)
                .HasForeignKey(ud => ud.DepartmentID);

            // Optional: enforce unique Role.Name and Department.Name if not handled by [Index]
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();
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
