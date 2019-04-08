using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index() => View(_repository.Products);

        public IActionResult UpdateProduct(long id)
        {
            ViewBag.Categories = _categoryRepository.Categories;
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
