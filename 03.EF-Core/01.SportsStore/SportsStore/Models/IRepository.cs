using System.Collections.Generic;
using SportsStore.Models.Pages;

namespace SportsStore.Models
{
    public interface IRepository
    {
        IEnumerable<Product> AllProducts { get;  }
        PagedList<Product> GetProducts(QueryOptions options, long categoryId = 0);
        Product GetProduct(long id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void Delete(Product product);
    }
}
