using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository) => _repository = repository;

        public IActionResult Index() => View(_repository.Products);

        public IActionResult UpdateProduct(long id)
        {
            return View(id == 0 ? new Product() : _repository.GetProduct(id));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                _repository.AddProduct(product);
            }
            else
            {
                _repository.UpdateProduct(product);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _repository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
