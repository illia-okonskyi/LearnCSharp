using System.Collections.Generic;

namespace CodeFirst.Models
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(long id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(long id);
    }
}
