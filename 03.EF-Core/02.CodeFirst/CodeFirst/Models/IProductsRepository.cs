using System.Collections.Generic;

namespace CodeFirst.Models
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts(string category = null, decimal? minPrice = null);
        Product GetProduct(long id);
        void CreateProduct(Product newProduct);
        void UpdateProduct(Product product);
        void UpdateProductWithChangesTracking(Product product, Product oldProduct);
        void DeleteProduct(long id);
    }
}
