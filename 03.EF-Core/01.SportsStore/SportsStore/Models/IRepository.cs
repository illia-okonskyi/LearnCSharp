using System.Collections.Generic;

namespace SportsStore.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProduct(long id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void Delete(Product product);
    }
}
