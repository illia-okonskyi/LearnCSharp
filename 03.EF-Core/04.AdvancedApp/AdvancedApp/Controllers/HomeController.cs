using Microsoft.AspNetCore.Mvc;
using AdvancedApp.Models;

namespace AdvancedApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdvancedContext _context;

        public HomeController(AdvancedContext context) => _context = context;

        public IActionResult Index()
        {
            return View(_context.Employees);
        }

        public IActionResult Edit(long id)
        {
            return View(id == default(long) ? new Employee() : _context.Employees.Find(id));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            if (employee.Id == default(long))
            {
                _context.Add(employee);
            }
            else
            {
                _context.Update(employee);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
