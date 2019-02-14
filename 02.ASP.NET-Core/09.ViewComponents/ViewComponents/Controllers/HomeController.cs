using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;

namespace ViewComponents.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;

        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            _repository.AddProduct(newProduct);
            return RedirectToAction(nameof(Index));
        }
    }
}
