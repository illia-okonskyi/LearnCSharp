using Microsoft.AspNetCore.Mvc;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public HomeController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IActionResult Index(string category = null, decimal? minPrice = null)
        {
            var products = _productsRepository.GetAllProducts(category, minPrice);
            ViewBag.Category = category;
            ViewBag.MinPrice = minPrice;
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("Editor", new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productsRepository.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(long id)
        {
            ViewBag.CreateMode = false;
            return View("Editor", _productsRepository.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Edit(Product product, Product old)
        {
            //_productsRepository.UpdateProduct(product);
            _productsRepository.UpdateProductWithChangesTracking(product, old);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            _productsRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
