using Microsoft.AspNetCore.Mvc;
using TagHelpers.Models;

namespace TagHelpers.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Cities);

        public ViewResult Create() => View();

        [HttpPost]
        public IActionResult Create(City city)
        {
            _repository.AddCity(city);
            return RedirectToAction(nameof(Index));
        }
    }
}
