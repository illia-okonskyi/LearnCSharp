using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ViewComponents.Models;

namespace ViewComponents.Controllers
{
    [ViewComponent(Name = "ComboComponent")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;

        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Create() => View();

        [HttpPost]
        public IActionResult Create(City newCity)
        {
            _repository.AddCity(newCity);
            return RedirectToAction("Index", "Home");
        }

        public IViewComponentResult Invoke() => new ViewViewComponentResult()
        {
            ViewData = new ViewDataDictionary<IEnumerable<City>>(ViewData, _repository.Cities)
        };
    }
}
