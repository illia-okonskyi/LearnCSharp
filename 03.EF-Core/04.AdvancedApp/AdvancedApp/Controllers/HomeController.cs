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
            ViewBag.Secondaries = _context.Set<SecondaryIdentity>();
            return View(_context.Employees
                .Include(e => e.OtherIdentity)
                .OrderByDescending(e => EF.Property<DateTime>(e, "LastUpdated")));
        }

        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            return View(string.IsNullOrWhiteSpace(SSN)
                ? new Employee()
                : _context.Employees
                    .Include(e => e.OtherIdentity)
                    .First(e => e.SSN == SSN && e.FirstName == firstName && e.FamilyName == familyName));
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            var hasExisting = _context.Employees
                .Count(e => e.SSN == employee.SSN &&
                    e.FirstName == employee.FirstName &&
                    e.FamilyName == employee.FamilyName) > 0;
            if (!hasExisting)
            {
                _context.Add(employee);
            }
            else
            {
                // It is important not to query the database for the current RowVersion value
                // All updates — regardless of the properties they modify—will cause a change that
                // can be used to detect concurrent updates
                var e = new Employee
                {
                    SSN = employee.SSN,
                    FirstName = employee.FirstName,
                    FamilyName = employee.FamilyName,
                    RowVersion = employee.RowVersion
                };
                _context.Employees.Attach(e);
                e.Salary = employee.Salary;
                e.LastUpdated = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            // NOTE: For ClientSetNull delete behavior: 
            //       Force load the target entity for consequent delete
            //       Isn't required if there is the PK property is provided in the query before
            //_context.Set<SecondaryIdentity>()
            //    .FirstOrDefault(id =>
            //        id.PrimarySSN == employee.SSN &&
            //        id.PrimaryFirstName == employee.FirstName &&
            //        id.PrimaryFamilyName == employee.FamilyName);

            // Re-creating Cascade delete behavior for Restrict delete behavior. We already have
            // all requred data (OtherIdentity.Id property) delivered from the MVC model binder
            //if (employee.OtherIdentity != null)
            //{
            //    _context.Set<SecondaryIdentity>().Remove(employee.OtherIdentity);
            //}

            // Re-creating SetNull/ClientSetNull delete behavior for Restrict delete behavior.
            // We have must have the OtherIdentity.Id property delivered from the MVC model binder
            if (employee.OtherIdentity != null)
            {
                var identity = _context.Set<SecondaryIdentity>().Find(employee.OtherIdentity.Id);
                identity.PrimarySSN = null;
                identity.PrimaryFirstName = null;
                identity.PrimaryFamilyName = null;
            }
            employee.OtherIdentity = null;

            _context.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
