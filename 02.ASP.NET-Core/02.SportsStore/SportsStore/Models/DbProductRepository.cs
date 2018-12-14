using System.Linq;

namespace SportsStore.Models
{
    public class DbProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DbProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;
    }
}
