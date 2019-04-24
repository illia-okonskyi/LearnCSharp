using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class DbWebServiceRepository : IWebServiceRepository
    {
        private AppDbContext _dbContext;
        public DbWebServiceRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public object GetProduct(long id)
        {
            return _dbContext.Products.Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.PurchasePrice,
                    p.RetailPrice,
                    p.CategoryId,
                    Category = new
                    {
                        p.Category.Id,
                        p.Category.Name,
                        p.Category.Description
                    }
                }).FirstOrDefault(p => p.Id == id);
        }

        public object GetProducts(int skip, int take)
        {
            return _dbContext.Products.Include(p => p.Category)
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.PurchasePrice,
                    p.RetailPrice,
                    p.CategoryId,
                    Category = new
                    {
                        p.Category.Id,
                        p.Category.Name,
                        p.Category.Description
                    }
                });
        }

        public long StoreProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product.Id;
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            _dbContext.Products.Remove(new Product { Id = id });
            _dbContext.SaveChanges();
        }
    }
}
