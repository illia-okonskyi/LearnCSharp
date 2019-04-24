using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Route("api/products")]
    public class WebServiceContoller : Controller
    {
        private IWebServiceRepository _repository;
        public WebServiceContoller(IWebServiceRepository repository) => _repository = repository;

        [HttpGet("{id}")]
        public object GetProduct(long id)
        {
            return _repository.GetProduct(id) ?? NotFound();
        }

        [HttpGet]
        public object Products(int skip, int take)
        {
            return _repository.GetProducts(skip, take);
        }

        [HttpPost]
        public long StoreProduct([FromBody] Product product)
        {
            return _repository.StoreProduct(product);
        }

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _repository.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            _repository.DeleteProduct(id);
        }
    }
}
