using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;

namespace ApiControllers.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository) => _repository = repository;

        public ViewResult Index() => View(_repository.Reservations);

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            _repository.AddReservation(reservation);
            return RedirectToAction(nameof(Index));
        }
    }
}
