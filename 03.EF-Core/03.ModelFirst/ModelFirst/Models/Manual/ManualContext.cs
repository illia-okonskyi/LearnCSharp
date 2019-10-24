using Microsoft.EntityFrameworkCore;

namespace ModelFirst.Models.Manual
{
    public class ManualContext : DbContext
    {
        public ManualContext(DbContextOptions<ManualContext> options)
            : base(options) { }

        public DbSet<Shoe> Shoes { get; set; }
    }
}
