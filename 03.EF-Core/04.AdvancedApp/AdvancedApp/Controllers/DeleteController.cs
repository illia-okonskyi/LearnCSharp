using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvancedApp.Models;

namespace AdvancedApp.Controllers
{
    // NOTE: There is a method of IQueryable to ignore any query filter for entity:
    //       IgnoreQueryFilters()

    public class DeleteController : Controller
    {
        private readonly AdvancedContext _context;

        public DeleteController(AdvancedContext context) => _context = context;

        public IActionResult Index()
        {
            return View(_context.Employees
                .Where(e => e.SoftDeleted)
                .Include(e => e.OtherIdentity)
                .IgnoreQueryFilters());
        }

        [HttpPost]
        public IActionResult Restore(Employee employee)
        {
            _context.Employees.IgnoreQueryFilters()
                .First(e =>
                    e.SSN == employee.SSN &&
                    e.FirstName == employee.FirstName &&
                    e.FamilyName == employee.FamilyName)
                .SoftDeleted = false;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
