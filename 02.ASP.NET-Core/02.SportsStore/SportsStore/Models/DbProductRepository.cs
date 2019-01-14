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

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return;
            }

            Product dbProduct = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
            if (dbProduct == null)
            {
                return;
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;
            dbProduct.Category = product.Category;
            _dbContext.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            var dbProduct = _dbContext.Products.FirstOrDefault(p => p.Id == productID);
            if (dbProduct != null)
            {
                _dbContext.Products.Remove(dbProduct);
                _dbContext.SaveChanges();
            }
            return dbProduct;
        }
    }
}
