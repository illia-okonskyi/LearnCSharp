using System.Collections.Generic;

namespace SportsStore.Models
{
    public class DbRepository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public DbRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public IEnumerable<Product> Products => _dbContext.Products;

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
    }
}
