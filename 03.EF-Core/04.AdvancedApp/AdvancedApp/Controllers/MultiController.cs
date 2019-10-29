using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdvancedApp.Models;

namespace AdvancedApp.Controllers
{
    public class MultiController : Controller
    {
        private readonly AdvancedContext _context;
        private readonly ILogger<MultiController> _logger;
        public MultiController(AdvancedContext context, ILogger<MultiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("EditAll", _context.Employees);
        }

        [HttpPost]
        public IActionResult UpdateAll(Employee[] employees)
        {
            _context.UpdateRange(employees);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
