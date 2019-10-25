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

        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            return View(string.IsNullOrWhiteSpace(SSN)
                ? new Employee() : _context.Employees.Include(e => e.OtherIdentity)
                .First(e => e.SSN == SSN && e.FirstName == firstName && e.FamilyName == familyName));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
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
