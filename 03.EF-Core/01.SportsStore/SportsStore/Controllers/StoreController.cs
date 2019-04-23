using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public StoreController(
            IRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(
            [FromQuery(Name = "options")] QueryOptions productOptions,
            QueryOptions categoryOptions,
            long categoryId)
        {
            ViewBag.Categories = _categoryRepository.GetCategories(categoryOptions);
            ViewBag.SelectedCategoryId = categoryId;
            return View(_productRepository.GetProducts(productOptions, categoryId));
        }
    }
}
