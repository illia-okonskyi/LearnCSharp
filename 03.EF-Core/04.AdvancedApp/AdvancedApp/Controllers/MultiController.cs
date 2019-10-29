using System;
using System.Linq;
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
            // NOTE: Transaction class implements Disposable pattern and can be used via using.
            //       A transaction is automatically rolled back when it is disposed. Example:
            //       using (var transaction = context.Database.BeginTransaction()) {
            //           ...operations are performed...
            //           context.Database.CommitTransaction();
            //       }
            // NOTE: Transaction isolation levels:
            //       - Chaos - This value is poorly defined, but when it is supported, it typically
            //                 behaves as though transactions are disabled.Changes made to the
            //                 database are visible to other clients before they are committed, and
            //                 there is often no rollback support. SQL Server does not support this
            //                 isolation level.
            //       - ReadUncomitted - This value represents the lowest level of isolation that is
            //                          commonly supported. Transactions using this isolation level
            //                          can read changes made by other transactions that have not
            //                          been committed.
            //       - ReadComitted - This value is the default level of isolation that is used if
            //                        no value is specified.Other transactions can still insert or
            //                        delete data between the updates made by the current
            //                        transaction, which can result in inconsistent query results.
            //       - RepeatableRead - This value represents a higher level of isolation that
            //                          prevents other transactions from modifying data that the
            //                          current transaction has read, which ensures consistent
            //                          query results.
            //       - Serializable - This value increases the RepeatableRead isolation level by
            //                        preventing other transactions from adding data to regions of
            //                        the database that have been read by the current transaction.
            //       - Snapshot - This value represents the highest level of isolation and ensures
            //                    that transactions each work with their own data snapshot. This
            //                    isolation level requires a change to the database that cannot be
            //                    performed using EF Core for current moment.
            _context.Database.BeginTransaction(); // isolationLevel argument values and extension
                                                  // is method declared in
                                                  //  Microsoft.EntityFrameworkCore namespace
                                                  //_context.Database.CurrentTransaction can be used to access current transaction
            _context.UpdateRange(employees);
            _context.SaveChanges();
            if (_context.Employees.Sum(e => e.Salary) < 1_000_000)
            {
                try
                {
                    _context.Database.CommitTransaction();
                }
                catch (Exception)
                {
                    _context.Database.RollbackTransaction();
                }

            }
            else
            {
                _context.Database.RollbackTransaction();
                throw new Exception("Salary total exceeds limit");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

