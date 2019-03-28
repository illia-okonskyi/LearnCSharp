using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class DbRepository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public DbRepository(AppDbContext dbContext) => _dbContext = dbContext;

        // NOTE: Force cast DbSet to List (or Array) can be used to avoid multiple DB queries when
        //       LINQ is used to operate data. But it can be bad practice in real applications
        //       because all the table entries are loaded into memory. In real applications
        //       it is still usefull to avoid multiple DB queries and operate on memory data, but
        //       more complex approach must be used, which can include query builder, pagination,
        //       etc.
        public IEnumerable<Product> Products => _dbContext.Products.ToList();

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
    }
}
