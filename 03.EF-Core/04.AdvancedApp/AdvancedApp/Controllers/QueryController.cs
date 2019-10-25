using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AdvancedApp.Models;

namespace AdvancedApp.Controllers
{
    public class QueryController : Controller
    {
        private AdvancedContext _context;
        public QueryController(AdvancedContext context) => _context = context;

        public IActionResult ServerEval()
        {
            // Produces query:
            //   SELECT[e].[SSN], [e].[FirstName], [e].[FamilyName], [e].[Salary], [e].[SoftDeleted]
            //   FROM[Employees] AS[e]
            //   WHERE([e].[SoftDeleted] = 0) AND([e].[Salary] > 150000.0)
            // And no local(client) calculations
            return View("Query", _context.Employees.Where(e => e.Salary > 150_000));
        }

        public IActionResult ClientEval()
        {
            // Produces query:
            //   SELECT[e].[SSN], [e].[FirstName], [e].[FamilyName], [e].[Salary], [e].[SoftDeleted]
            //   FROM[Employees] AS[e]
            //   WHERE([e].[SoftDeleted] = 0)
            // And IsHighEarner(e) is calculated locally, the log warning message appears:
            //   The LINQ expression 'where value(AdvancedApp.Controllers.QueryController)
            //   .IsHighEarner([e])' could not be translated and will be evaluated locally
            // To throw exception when such warning appears additional configuration of the
            // DbContext is needed like this (Startup.cs):
            // services.AddDbContext<AdvancedContext>(options =>
            //     options.UseSqlServer(connectionString).ConfigureWarnings(warning =>
            //         warning.Throw(RelationalEventId.QueryClientEvaluationWarning))
            // );
            return View("Query", _context.Employees.Where(e => IsHighEarner(e)));
        }

        private bool IsHighEarner(Employee e)
        {
            return e.Salary > 150_000;
        }
    }
}
