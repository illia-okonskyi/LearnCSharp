using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelFirst.Models.Manual;

namespace ModelFirst.Controllers
{
    public class ManualController : Controller
    {
        private readonly ManualContext _context;

        public ManualController(ManualContext context) => _context = context;

        public IActionResult Index()
        {
            ViewBag.Styles = _context.ShoeStyles.Include(s => s.Products);
            ViewBag.Widths = _context.ShoeWidths.Include(s => s.Products);
            ViewBag.Categories = _context.Categories.Include(c => c.Shoes).ThenInclude(j => j.Shoe);
            return View(
                _context.Shoes
                .Include(s => s.Style)
                .Include(s => s.Width)
                .Include(s => s.Categories)
                .ThenInclude(j => j.Category));
        }

        public IActionResult Edit(long id)
        {
            ViewBag.Styles = _context.ShoeStyles;
            ViewBag.Widths = _context.ShoeWidths;
            ViewBag.Categories = _context.Categories;
            return View(_context.Shoes
                .Include(s => s.Style)
                .Include(s => s.Campaign)
                .Include(s => s.Width)
                .Include(s => s.Categories)
                .ThenInclude(j => j.Category).First(s => s.Id == id));
        }

        [HttpPost]
        public IActionResult Update(
            Shoe shoe,
            long[] newCategoryIds,
            ShoeCategoryJunction[] oldJunctions)
        {
            var unchangedJunctions = oldJunctions.Where(j => newCategoryIds.Contains(j.CategoryId));
            _context.Set<ShoeCategoryJunction>().RemoveRange(oldJunctions.Except(unchangedJunctions));
            shoe.Categories = newCategoryIds.Except(unchangedJunctions.Select(j => j.CategoryId))
                .Select(id => new ShoeCategoryJunction
                {
                    ShoeId = shoe.Id,
                    CategoryId = id
                })
                .ToList();
            _context.Shoes.Update(shoe);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
