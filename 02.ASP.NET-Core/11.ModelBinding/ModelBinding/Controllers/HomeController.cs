using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var person = _repository[id.Value];
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
    }
}
