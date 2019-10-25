using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Snippet 1
        //public IActionResult Edit(long id)
        public IActionResult Edit(string SSN)
        {
            // Snippet 1
            //return View(id == default(long)
            //    ? new Employee()
            //    : _context.Employees.Include(e => e.OtherIdentity).First(e => e.Id == id));

            return View(string.IsNullOrWhiteSpace(SSN)
                ? new Employee()
                : _context.Employees.Include(e => e.OtherIdentity).First(e => e.SSN == SSN));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            // Snippet 1
            //if (employee.Id == default(long))
            if (_context.Employees.Count(e => e.SSN == employee.SSN) == 0)
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
