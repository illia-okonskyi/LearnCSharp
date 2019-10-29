using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvancedApp.Models;
using System.Collections.Generic;

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

        [HttpPost]
        public IActionResult Delete(Employee e)
        {
            // Re-created the cascade delete behavior
            if (e.OtherIdentity != null)
            {
                _context.Remove(e.OtherIdentity);
            }
            _context.Employees.Remove(e);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            // Re-created the cascade delete behavior
            IEnumerable<Employee> data = _context.Employees
                .IgnoreQueryFilters()
                .Include(e => e.OtherIdentity)
                .Where(e => e.SoftDeleted)
                .ToArray();
            _context.RemoveRange(data.Select(e => e.OtherIdentity));
            _context.RemoveRange(data);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RestoreAll()
        {
            // Calling stored procedures that doesn't return data can be done this way
            _context.Database.ExecuteSqlCommand("EXECUTE RestoreSoftDelete");
            return RedirectToAction(nameof(Index));
        }
    }
}
