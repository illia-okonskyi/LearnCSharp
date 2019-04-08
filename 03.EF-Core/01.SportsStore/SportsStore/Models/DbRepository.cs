using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Product> Products => _dbContext.Products.Include(p => p.Category).ToList();

        public Product GetProduct(long id)
        {
            return _dbContext.Products.Include(p => p.Category).First(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            // NOTE: EF Core has internal mechanism of tracking the changes in the queried objects
            //       but this mechanism works only on objects queried before update. Updating the
            //       object which is not queried before causes set on all of it's fields
            // NOTE: For updating the range of the elements there is a DbSet.UpdateRange method, but
            //       it updates all the fields of the all the elements in the range (same as
            //       DbSet.Update). To use the tracking changes mechanism all object in the range
            //       must be queried before.

            // 1) Update all fields. Only one UPDATE SQL commands
            _dbContext.Products.Update(product);

            // 2) Update only changed fields. Two SQL commands: SELECT and UPDATE
            //var storedProduct = GetProduct(product.Id);
            //storedProduct.Name = product.Name;
            //storedProduct.Category = product.Category;
            //storedProduct.PurchasePrice = product.PurchasePrice;
            //storedProduct.RetailPrice = product.RetailPrice;

            _dbContext.SaveChanges();
        }

        public void Delete(Product product)
        {
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
