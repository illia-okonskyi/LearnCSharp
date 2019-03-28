using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository) => _repository = repository;

        public IActionResult Index() => View(_repository.Products);

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _repository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long id)
        {
            return View(_repository.GetProduct(id));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _repository.UpdateProduct(product);
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
