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
            ViewBag.Suppliers = _dbContext.Suppliers.Include(s => s.Contact);
            return View("ContactEditor",
                _dbContext.Set<ContactDetails>()
                    .Include(cd => cd.Supplier)
                    .First(cd => cd.Id == id)
            );
        }

        [HttpPost]
        public IActionResult Update(
            ContactDetails details,
            long? targetSupplierId,
            long[] spares)
        {
            if (details.Id == 0)
            {
                _dbContext.Add<ContactDetails>(details);
            }
            else
            {
                _dbContext.Update<ContactDetails>(details);
                if (targetSupplierId.HasValue)
                {
                    if (spares.Contains(targetSupplierId.Value))
                    {
                        details.SupplierId = targetSupplierId.Value;
                    }
                    else
                    {
                        // NOTE: For swapping the relation in 1-to-1 relationships 4 steps are
                        //       required
                        //       1) old = current, unique is broken
                        //       2) current = temp.FK_Id = create new primary object (with id e.g.
                        //          15), unique is fixed
                        //       - save changes
                        //       3) current = old, unique is fixed
                        //       4) remove temp primary object (id = 15)
                        //       - save changes
                        // var targetDetails = _dbContext.Set<ContactDetails>()
                        //     .FirstOrDefault(cd => cd.SupplierId == targetSupplierId);
                        // targetDetails.SupplierId = details.Supplier.Id;
                        // Supplier temp = new Supplier { Name = "temp" };
                        // details.Supplier = temp;
                        // _dbContext.SaveChanges();
                        // details.SupplierId = targetSupplierId.Value;
                        // temp.Contact = null;
                        // _dbContext.Suppliers.Remove(temp);

                        // NOTE: Work with 1-to-0|1 relation is much simpler because there is no
                        //       unique foreign key requirement

                        var targetDetails = _dbContext.Set<ContactDetails>()
                            .FirstOrDefault(cd => cd.SupplierId == targetSupplierId);
                        targetDetails.SupplierId = null;
                        details.SupplierId = targetSupplierId.Value;
                        _dbContext.SaveChanges();
                    }
                }
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
