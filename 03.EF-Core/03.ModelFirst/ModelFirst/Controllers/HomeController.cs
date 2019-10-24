using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelFirst.Models.Scaffold;

namespace ModelFirst.Controllers
{
    public class HomeController : Controller
    {
        private ScaffoldContext _context;

        public HomeController(ScaffoldContext context) => _context = context;

        public IActionResult Index()
        {
            return View(_context.Shoes
                .Include(s => s.Color)
                .Include(s => s.SalesCampaigns)
                .Include(s => s.ShoeCategoryJunction)
                .ThenInclude(junct => junct.Category)
                .Include(s => s.Fitting));
        }
    }
}
