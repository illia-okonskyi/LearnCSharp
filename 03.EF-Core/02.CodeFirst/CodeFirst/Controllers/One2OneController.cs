using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    public class One2OneController : Controller
    {
        private EfDbContext _dbContext;

        public One2OneController(EfDbContext dbContext) => _dbContext = dbContext;

        public IActionResult Index()
        {
            return View(_dbContext.Set<ContactDetails>().Include(cd => cd.Supplier));
        }

        public IActionResult Create() => View("ContactEditor");
        public IActionResult Edit(long id)
        {
            return View("ContactEditor",
                _dbContext.Set<ContactDetails>()
                    .Include(cd => cd.Supplier)
                    .First(cd => cd.Id == id)
            );
        }

        [HttpPost]
        public IActionResult Update(ContactDetails details)
        {
            if (details.Id == 0)
            {
                _dbContext.Add<ContactDetails>(details);
            }
            else
            {
                _dbContext.Update<ContactDetails>(details);
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
