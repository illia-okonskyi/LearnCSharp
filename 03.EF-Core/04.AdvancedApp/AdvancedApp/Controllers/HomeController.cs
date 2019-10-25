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

        // Snippet 1-3
        //public IActionResult Edit(long id)
        // Snippet 4
        //public IActionResult Edit(string SSN)
        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            // Snippet 1-3
            //return View(id == default(long)
            //    ? new Employee()
            //    : _context.Employees.Include(e => e.OtherIdentity).First(e => e.Id == id));

            // Snippet 4
            //return View(string.IsNullOrWhiteSpace(SSN)
            //    ? new Employee()
            //    : _context.Employees.Include(e => e.OtherIdentity).First(e => e.SSN == SSN));

            return View(string.IsNullOrWhiteSpace(SSN)
                ? new Employee() : _context.Employees.Include(e => e.OtherIdentity)
                .First(e => e.SSN == SSN && e.FirstName == firstName && e.FamilyName == familyName));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            // Snippet 1-3
            //if (employee.Id == default(long))
            // Snippet 4
            //if (_context.Employees.Count(e => e.SSN == employee.SSN) == 0)
            if (_context.Employees.Count(e => 
                e.SSN == employee.SSN &&
                e.FirstName == employee.FirstName &&
                e.FamilyName == employee.FamilyName) == 0)
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
