using Microsoft.EntityFrameworkCore;
using pps_api.Entities;

namespace pps_api
{
    public class AppDbContext : DbContext
    {
        public DbSet<Identity> Identity { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

}
