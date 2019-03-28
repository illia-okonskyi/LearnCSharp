using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class MemoryRepository : IRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> Products => _products;

        public Product GetProduct(long id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var storedProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (storedProduct == null)
            {
                throw new ArgumentException(nameof(product));
            }

            storedProduct = product;
        }

        public void Delete(Product product)
        {
            _products.RemoveAll(p => p.Id == product.Id);
        }
    }
}
