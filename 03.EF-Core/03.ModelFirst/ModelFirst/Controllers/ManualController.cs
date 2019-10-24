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
    }
}
