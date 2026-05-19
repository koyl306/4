using Microsoft.EntityFrameworkCore;

namespace pract9
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Experiment> LabExp { get; set; }
    }
}
