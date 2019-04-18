using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(QueryOptions options) => View(_categoryRepository.GetCategories(options));

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditCategory(long id)
        {
            ViewBag.EditId = id;
            return View(nameof(Index), _categoryRepository.AllCategories);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteCategory(Category category)
        {
            _categoryRepository.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
