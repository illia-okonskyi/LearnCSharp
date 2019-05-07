using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {}

        public DbSet<Product> Products { get; set; }
    }
}
