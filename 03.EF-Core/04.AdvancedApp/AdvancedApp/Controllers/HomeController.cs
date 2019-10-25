using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvancedApp.Models;

namespace AdvancedApp.Controllers
{
    // NOTE: There are method of the IQueryable that controls the change tracking mechanism:
    //       - AsNoTracking() - This method disables change tracking for the results of the query
    //                          to which it is applied.
    //       - AsTracking() - This method enables change tracking for the results of the query to
    //                        which it is applied.

    public class HomeController : Controller
    {
        private readonly AdvancedContext _context;

        // NOTE: Entity Framework Core supports the SQL LIKE expression for IQueryable,
        //       which means that queries can be performed using search patterns.
        //       Wildcards are next:
        //       - % - This wildcard matches any string of zero or more characters.
        //       - _ - This wildcard matches any single character.
        //       - [chars] - This wildcard matches any single character within a set.
        //       - [^ chars] - This wildcard matches any single character not within a set.
        private static readonly Func<AdvancedContext, string, IEnumerable<Employee>> _query
            = EF.CompileQuery((AdvancedContext context, string searchTerm) =>
                context.Employees.Where(e => EF.Functions.Like(e.FirstName, searchTerm))
            );

        public HomeController(AdvancedContext context) => _context = context;

        public IActionResult Index(string searchTerm)
        {
            return View(string.IsNullOrEmpty(searchTerm)
                ? _context.Employees
                : _query(_context, searchTerm));
        }

        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            return View(string.IsNullOrWhiteSpace(SSN)
                ? new Employee()
                : _context.Employees
                    .Include(e => e.OtherIdentity)
                    .AsNoTracking()
                    .First(e => e.SSN == SSN && e.FirstName == firstName && e.FamilyName == familyName));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            var existing = _context.Employees
                .AsTracking()
                .First(e => e.SSN == employee.SSN && e.FirstName == employee.FirstName && e.FamilyName == employee.FamilyName);
            // NOTE: Now `existing` is being change tracked
            //       Change tracking for concrete entry can be disabled via next statement
            //       _context.Entry(existing).State = EntityState.Detached;

            if (existing == null)
            {
                _context.Add(employee);
            }
            else
            {
                existing.Salary = employee.Salary;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            _context.Attach(employee);
            employee.SoftDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
