using System.Collections.Generic;

namespace SportsStore.Models
{
    public class MemoryRepository : IRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> Products => _products;

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
    }
}
