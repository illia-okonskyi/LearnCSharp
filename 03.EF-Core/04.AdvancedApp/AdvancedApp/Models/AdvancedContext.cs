using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Models
{
    public class AdvancedContext : DbContext
    {
        public AdvancedContext(DbContextOptions<AdvancedContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
