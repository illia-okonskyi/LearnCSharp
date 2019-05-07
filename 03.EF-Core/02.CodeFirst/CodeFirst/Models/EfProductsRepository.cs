using System;
using System.Collections.Generic;

namespace CodeFirst.Models
{
    public class EfProductsRepository : IProductsRepository
    {
        private readonly EfDbContext _dbContext;

        public EfProductsRepository(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products;
        }

        public Product GetProduct(long id)
        {
            throw new NotImplementedException();
        }

        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(long id)
        {
            throw new NotImplementedException();
        }
    }
}
