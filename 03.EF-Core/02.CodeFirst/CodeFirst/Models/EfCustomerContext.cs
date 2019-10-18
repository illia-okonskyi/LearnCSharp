using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models
{
    public class EfCustomerContext : DbContext
    {
        public EfCustomerContext(DbContextOptions<EfCustomerContext> opts)
            : base(opts)
        { }

        public DbSet<Customer> Customers { get; set; }
    }
}
