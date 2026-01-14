using Microsoft.EntityFrameworkCore;

namespace AddSolBackend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // This creates a table called "Employees"
        public DbSet<Employee> Employees { get; set; }
    }
}
