using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models
{
    public class EfProductsRepository : IProductsRepository
    {
        private readonly EfDbContext _dbContext;

        public EfProductsRepository(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts(
            string category = null,
            decimal? minPrice = null,
            bool includeRelated = true)
        {
            Debug.WriteLine($"ProductsRepo> GetAllProducts({category?? "<null>"}, {minPrice ?? 0})");
            IQueryable<Product> products = _dbContext.Products;
            if (category != null)
            {
                products = products.Where(p => p.Category == category);
            }
            if (minPrice != null && minPrice > 0)
            {
                products = products.Where(p => p.Price >= minPrice);
            }
            if (includeRelated)
            {
                products = products.Include(p => p.Supplier);
            }
            return products;
        }

        public Product GetProduct(long id)
        {
            Debug.WriteLine($"ProductsRepo> GetProduct({id})");
            return _dbContext.Products.Include(p => p.Supplier).First(p => p.Id == id);
        }

        public void CreateProduct(Product newProduct)
        {
            Debug.WriteLine($"ProductsRepo> CreateProduct");
            // NOTE: The Id property must be explicitly set to 0 to create new object entry
            //       in the DB
            newProduct.Id = 0;
            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
            Debug.WriteLine($"ProductsRepo> - Created product ID = {newProduct.Id}");

        }

        public void UpdateProduct(Product product)
        {
            Debug.WriteLine($"ProductsRepo> UpdateProduct");
            _dbContext.Update(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProductWithChangesTracking(Product product, Product oldProduct)
        {
            Debug.WriteLine($"ProductsRepo> UpdateProductWithChangesTracking");
            // 1) Attach or Find existing db entity
            Product dbEntity = oldProduct;
            if (dbEntity == null)
            {
                dbEntity = _dbContext.Products.Find(product.Id);
            }
            else
            {
                _dbContext.Products.Attach(dbEntity);
            }

            // 2) Modify values of the entity with passed ones via variable
            dbEntity.Name = product.Name;
            dbEntity.Category = product.Category;
            dbEntity.Price = product.Price;
            dbEntity.Supplier.Name = product.Supplier.Name;
            dbEntity.Supplier.City = product.Supplier.City;
            dbEntity.Supplier.State = product.Supplier.State;

            // Debug: log entity entry state
            var entry = _dbContext.Entry(dbEntity);
            Debug.WriteLine($"ProductsRepo> - Entity State: {entry.State}");
            foreach (string propName in new string[] { "Name", "Category", "Price" })
            {
                Debug.WriteLine($"ProductsRepo> - Property: {propName} - " +
                    $"Old: {entry.OriginalValues[propName]}, " +
                    $"New: {entry.CurrentValues[propName]}");
            }

            // 3) SaveChanges now will track changes and stores only changed values of the db entity
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            Debug.WriteLine($"ProductsRepo> DeleteProduct({id})");
            // NOTE: Only the key is used to identify the row in the database that will be deleted,
            //       so the delete operation can be performed by creating a new Product object with
            //       just an Id value and passing it to the Remove method.
            _dbContext.Products.Remove(new Product { Id = id });
            _dbContext.SaveChanges();
        }
    }
}
