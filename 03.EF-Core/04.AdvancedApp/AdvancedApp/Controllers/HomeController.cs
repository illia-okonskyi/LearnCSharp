﻿using System.Linq;
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

        public HomeController(AdvancedContext context) => _context = context;

        public IActionResult Index()
        {
            return View(_context.Employees.AsNoTracking());
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
    }
}